using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StringingController : ControllerBase
    {
        private readonly IStringingOrder _stringingBL;

        public StringingController(IStringingOrder stringingBL)
        {
            _stringingBL = stringingBL;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_stringingBL.GetAll());

        [HttpPost]
        public IActionResult Create(StringingOrder order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = _stringingBL.Create(order);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            var order = _stringingBL.UpdateStatus(id, status);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
}