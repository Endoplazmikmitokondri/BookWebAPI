using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
using OnlineBookStore.Services;
using OnlineBookStore.Dtos;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var result = await _authService.RegisterAsync(user);
            if (!result.Success)
            {
                return BadRequest(new { result.Success, result.Message });
            }
            return Ok(new { result.Success, result.Message, result.Token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var result = await _authService.LoginAsync(user);

            if (!result.Success)
            {
                return Unauthorized(new { result.Success, result.Message });
            }

            return Ok(new { result.Success, result.Message, Token = result.Token });
        }
    }
}
