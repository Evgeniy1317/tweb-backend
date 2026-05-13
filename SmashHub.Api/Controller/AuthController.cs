using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmashHub.Api.Services;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain.Models.User;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUser _userBL;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(IUser userBL, JwtTokenService jwtTokenService)
        {
            _userBL = userBL;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_userBL.EmailExists(model.Email)) return BadRequest("Email already exists");
            var created = _userBL.UserRegister(model);
            return Ok(created);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(UserLoginModel model)
        {
            var user = _userBL.UserLogin(model);
            if (user.Id == 0) return Unauthorized("Invalid credentials");
            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new AuthResponseModel
            {
                Token = token,
                User = user
            });
        }

        [HttpGet("profile")]
        [Authorize]
        public IActionResult Profile()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var user = _userBL.GetProfile(userId.Value);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPatch("profile")]
        [Authorize]
        public IActionResult UpdateProfile(UserProfileUpdateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var updated = _userBL.UpdateProfile(userId.Value, model);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        private int? GetCurrentUserId()
        {
            var rawId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(rawId, out var userId) ? userId : null;
        }
    }
}
