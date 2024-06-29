using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
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

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            var registeredUser = await _authService.RegisterUserAsync(user);
            return Ok(registeredUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] User user)
        {
            var token = await _authService.LoginUserAsync(user.Username, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
