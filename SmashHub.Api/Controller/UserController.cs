using Microsoft.AspNetCore.Mvc;
using SmashHub.Domain;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static List<User> _users = new List<User>();

        [HttpGet]
        public IActionResult GetAll() => Ok(_users);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_users.Any(u => u.Email == user.Email))
                return BadRequest("Email already exists");

            _users.Add(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User updated)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_users.Any(u => u.Email == updated.Email && u.Id != id))
                return BadRequest("Email already taken by another user");

            user.Name = updated.Name;
            user.Email = updated.Email;
            user.Phone = updated.Phone;
            user.Avatar = updated.Avatar;

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            _users.Remove(user);
            return NoContent();
        }
    }
}