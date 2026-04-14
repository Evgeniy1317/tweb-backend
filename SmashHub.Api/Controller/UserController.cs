using Microsoft.AspNetCore.Mvc;
using SmashHub.Api.Models;

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
            if (string.IsNullOrWhiteSpace(user.Name))
                return BadRequest("Name is required");

            if (user.Name.Length < 3 || user.Name.Length > 50)
                return BadRequest("Name must be between 3 and 50 characters");

            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("Email is required");

            if (!user.Email.Contains("@") || !user.Email.Contains("."))
                return BadRequest("Email is not valid");

            if (string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Password is required");

            if (user.Password.Length < 6)
                return BadRequest("Password must be at least 6 characters");

            if (_users.Any(u => u.Email == user.Email))
                return BadRequest("Email already exists");

            _users.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User updated)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            if (string.IsNullOrWhiteSpace(updated.Name))
                return BadRequest("Name is required");

            if (updated.Name.Length < 3 || updated.Name.Length > 50)
                return BadRequest("Name must be between 3 and 50 characters");

            if (string.IsNullOrWhiteSpace(updated.Email))
                return BadRequest("Email is required");

            if (!updated.Email.Contains("@") || !updated.Email.Contains("."))
                return BadRequest("Email is not valid");

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