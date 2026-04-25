using Microsoft.AspNetCore.Mvc;
using blogWithAPI.Entity.Concrete;
using blogWithAPI.Entity.Results;
using blogWithAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Duende.IdentityModel.Client;


using System.Security.Claims;
using blogWithAPI.DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace blogWithAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IHttpClientFactory httpClientFactory, Context context, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpClientFactory = httpClientFactory;
            _context = context;
            _configuration = configuration;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Name = registerDto.FirstName,
                Surname = registerDto.LastName,
                ImageUrl = ""
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {

                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new AppRole { Name = "Admin" });
                }
                
                await _userManager.AddToRoleAsync(user, "Admin");

                return Ok(new SuccessResult("Kullanıcı başarıyla kaydedildi ve Admin olarak atandı."));
            }
            return BadRequest(new ErrorResult("Kullanıcı kaydedilemedi."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return BadRequest(new ErrorResult("Kullanıcı bulunamadı."));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResult("Şifre hatalı."));
            }

            var client = _httpClientFactory.CreateClient("InsecureClient");
            
            var authority = _configuration["Identity:Authority"];
            
            var discovery = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = authority,
                Policy = new Duende.IdentityModel.Client.DiscoveryPolicy { RequireHttps = authority.StartsWith("https") }
            });
            if (discovery.IsError) return BadRequest(new ErrorResult(discovery.Error ?? "Discovery hatası oluştu."));

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discovery.TokenEndpoint,
                UserName = user.UserName!, // IdentityServer kullanıcı adını (username) bekler
                Password = loginDto.Password,
                ClientId = "blogClient",
                ClientSecret = "secret",
                Scope = "blogapi offline_access"
            });

            if (tokenResponse.IsError) return BadRequest(new ErrorResult("Token alınamadı: " + tokenResponse.Error));

            var refreshToken = new UserRefreshToken
            {
                Code = tokenResponse.RefreshToken ?? Guid.NewGuid().ToString(),
                UserId = user.Id,
                Expiration = DateTime.Now.AddDays(7)
            };

            _context.UserRefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new SuccessDataResult<TokenResponseDTO>(new TokenResponseDTO
            {
                 AccessToken = tokenResponse.AccessToken ?? "",
                 RefreshToken = refreshToken.Code,
                 Expiration = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn)
            }, "Token başarıyla alındı."));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshTokenCode)
        {
            var existToken = await _context.UserRefreshTokens
                .FirstOrDefaultAsync(x => x.Code == refreshTokenCode);

            if (existToken == null || existToken.Expiration < DateTime.Now)
            {
                return BadRequest(new ErrorResult("Refresh token geçersiz veya süresi dolmuş."));
            }

            var user = await _userManager.FindByIdAsync(existToken.UserId.ToString());
            if (user == null) return BadRequest(new ErrorResult("Kullanıcı bulunamadı."));

            var client = _httpClientFactory.CreateClient("InsecureClient");
            var authority = _configuration["Identity:Authority"];

            var discovery = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = authority,
                Policy = new Duende.IdentityModel.Client.DiscoveryPolicy { RequireHttps = authority.StartsWith("https") }
            });
            if (discovery.IsError) return BadRequest(new ErrorResult(discovery.Error ?? "Discovery hatası oluştu."));

            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = "blogClient",
                ClientSecret = "secret",
                RefreshToken = existToken.Code
            });
            
            if (tokenResponse.IsError) return BadRequest(new ErrorResult("Token yenilenemedi: " + tokenResponse.Error));

            existToken.Code = tokenResponse.RefreshToken ?? existToken.Code;
            existToken.Expiration = DateTime.Now.AddDays(7);
            
            _context.UserRefreshTokens.Update(existToken);
            await _context.SaveChangesAsync();

            return Ok(new SuccessDataResult<TokenResponseDTO>(new TokenResponseDTO
            {
                AccessToken = tokenResponse.AccessToken ?? "",
                RefreshToken = existToken.Code,
                Expiration = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn)
            }, "Token başarıyla yenilendi."));
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser? user = null;
            if (!string.IsNullOrEmpty(userId)) user = await _userManager.FindByIdAsync(userId);
            if (user == null) user = await _userManager.FindByNameAsync(User.Identity?.Name ?? "");

            if (user == null) return Unauthorized(new ErrorResult("Kullanıcı kimliği doğrulanamadı."));

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new SuccessDataResult<object>(new 
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "Admin" 
            }, "Kullanıcı bilgileri getirildi."));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new SuccessResult("Kullanıcı başarıyla çıkış yaptı."));
        }
    } 
}
