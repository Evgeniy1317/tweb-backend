using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic.Interfaces;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userBL;

        public UserController(IUser userBL)
        {
            _userBL = userBL;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userBL.GetProfile(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_userBL.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
