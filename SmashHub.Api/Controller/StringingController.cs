using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StringingController : ControllerBase
    {
        private readonly IStringingOrder _orderBL;

        public StringingController()
        {
            var bl = new BussinesLogic();
            _orderBL = bl.GetStringingOrderBL();
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_orderBL.GetAll());

        [HttpPost]
        public IActionResult Create(StringingOrder order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(_orderBL.Create(order));
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] StatusUpdateRequest request)
        {
            var order = _orderBL.UpdateStatus(id, request.Status);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }

    public class StatusUpdateRequest
    {
        public string Status { get; set; } = string.Empty;
    }
}