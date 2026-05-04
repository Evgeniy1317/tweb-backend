using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournament _tournamentBL;

        public TournamentsController(ITournament tournamentBL)
        {
            _tournamentBL = tournamentBL;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_tournamentBL.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tournament = _tournamentBL.GetById(id);
            if (tournament == null) return NotFound();
            return Ok(tournament);
        }

        [HttpPost]
        public IActionResult Create(Tournament tournament)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = _tournamentBL.Create(tournament);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Tournament updated)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var tournament = _tournamentBL.Update(id, updated);
            if (tournament == null) return NotFound();
            return Ok(tournament);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_tournamentBL.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}