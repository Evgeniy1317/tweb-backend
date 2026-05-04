using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourtsController : ControllerBase
    {
        private readonly ICourt _courtBL;

        public CourtsController(ICourt courtBL)
        {
            _courtBL = courtBL;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_courtBL.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var court = _courtBL.GetById(id);
            if (court == null) return NotFound();
            return Ok(court);
        }

        [HttpPost]
        public IActionResult Create(Court court)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = _courtBL.Create(court);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Court updated)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var court = _courtBL.Update(id, updated);
            if (court == null) return NotFound();
            return Ok(court);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_courtBL.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}