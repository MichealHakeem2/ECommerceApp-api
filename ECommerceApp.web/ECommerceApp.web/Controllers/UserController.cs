using ECommerceApp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string fullName, string email, string password)
        {
            await _userService.RegisterUserAsync(fullName, email, password);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userService.ValidateUserAsync(email, password);
            if (user == null) return Unauthorized("Invalid email or password");

            // ⚠️ later you should replace with JWT tokens instead of raw user
            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // in API, logout means client should discard the token
            return Ok("Logged out successfully");
        }
    }
}
