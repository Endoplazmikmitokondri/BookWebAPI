using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Services;
using System.Threading.Tasks;

namespace OnlineBookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authService.Authenticate(loginDto.Username, loginDto.Password);

            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
