using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain.Models.User;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUser _userBL;

        public AuthController(IUser userBL)
        {
            _userBL = userBL;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_userBL.EmailExists(model.Email)) return BadRequest("Email already exists");
            var created = _userBL.UserRegister(model);
            return Ok(created);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginModel model)
        {
            var user = _userBL.UserLogin(model);
            if (user.Id == 0) return Unauthorized("Invalid credentials");
            return Ok(user);
        }

        [HttpGet("profile")]
        public IActionResult Profile([FromQuery] int userId)
        {
            var user = _userBL.GetById(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}