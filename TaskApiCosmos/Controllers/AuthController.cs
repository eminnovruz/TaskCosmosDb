using Microsoft.AspNetCore.Mvc;
using TaskApiCosmos.Models.DTO;
using TaskApiCosmos.Services.Interfaces;

namespace TaskApiCosmos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTService _jwtService;

        public AuthController(IUserService userService, IJWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromForm] RegisterDTO request)
        {
            var existingUser = await _userService.FindUserByEmailAsync(request.Email);
            if (existingUser is not null)
                return Conflict("User already exists");

            var user = await _userService.RegisterAsync(request);
            if (user is not null)
                return GenerateAccessToken(user.Id.ToString(), user.Email);

            return BadRequest(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO request)
        {
            var user = await _userService.FindUserByEmailAsync(request.Email);
            if (user is null)
                return Conflict("User doesn't exist");

            var result = await _userService.LoginAsync(request);

            if (!result)
                return Conflict("Password or email is incorrect");

            return GenerateAccessToken(user.Id.ToString(), user.Email);
        }

        private string GenerateAccessToken(string id, string email)
        {
            var accessToken = _jwtService.GenerateSecurityToken(id, email);
            return accessToken;
        }
    }
}
