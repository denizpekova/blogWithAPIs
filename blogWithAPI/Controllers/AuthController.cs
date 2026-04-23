using Microsoft.AspNetCore.Mvc;
using blogWithAPI.Entity.Concrete;
using blogWithAPI.Entity.Results;
using blogWithAPI.Model;
using Microsoft.AspNetCore.Identity;
using Duende.IdentityModel.Client; // 'IdentityModel.Client' yerine bunu yazın


namespace blogWithAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                ImageUrl = "" // Boş kalmaması için ekleyelim

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return Ok(new SuccessResult("Kullanıcı başarıyla kaydedildi."));
            }
            return BadRequest(new ErrorResult("Kullanıcı kaydedilemedi."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
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
            var discovery = await client.GetDiscoveryDocumentAsync("http://blog.mtapi.com.tr");
            if (discovery.IsError) return BadRequest(new ErrorResult(discovery.Error));

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discovery.TokenEndpoint,
                UserName = loginDto.UserName,
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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new SuccessResult("Kullanıcı başarıyla çıkış yaptı."));
        }
    } 
}
