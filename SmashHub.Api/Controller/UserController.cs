using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;
using SmashHub.Helpers;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userBL;

        public UserController()
        {
            var bl = new BussinesLogic();
            _userBL = bl.GetUserBL();
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_userBL.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userBL.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(UserRegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_userBL.EmailExists(model.Email)) return BadRequest("Email already exists");
            var created = _userBL.UserRegister(model);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User updated)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = _userBL.GetById(id);
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