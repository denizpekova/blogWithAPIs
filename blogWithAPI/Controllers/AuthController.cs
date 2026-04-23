using Microsoft.AspNetCore.Mvc;
using blogWithAPI.Entity.Concrete;
using blogWithAPI.Entity.Results;
using blogWithAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Duende.IdentityModel.Client;


using System.Security.Claims;

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

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpClientFactory = httpClientFactory;
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
                // Admin rolü yoksa oluştur
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new AppRole { Name = "Admin" });
                }
                // Kullanıcıya Admin rolünü ata
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

            var client = _httpClientFactory.CreateClient();
            
            // İstek localhost'tan geliyorsa localhost'u, değilse domaini kullan
            var authority = HttpContext.Request.Host.Host == "localhost" 
                ? "http://localhost:5279" 
                : "http://blog.mtapi.com.tr";

            var discovery = await client.GetDiscoveryDocumentAsync(authority);
            if (discovery.IsError) return BadRequest(new ErrorResult(discovery.Error ?? "Discovery hatası oluştu."));

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discovery.TokenEndpoint,
                UserName = user.UserName!, // IdentityServer kullanıcı adını (username) bekler
                Password = loginDto.Password,
                ClientId = "blogClient",
                ClientSecret = "secret",
                Scope = "blogapi"
            });

            if (tokenResponse.IsError) return BadRequest(new ErrorResult("Token alınamadı: " + tokenResponse.Error));

            return Ok(new SuccessDataResult<object>(new 
            {
                 Token = tokenResponse.AccessToken,
                 ExpiresIn = tokenResponse.ExpiresIn,
                 User = user.UserName
            }, "Token başarıyla alındı."));
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
