using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain.Models.Stringing;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StringingController : ControllerBase
    {
        private static readonly HashSet<string> AllowedStatuses = new(StringComparer.OrdinalIgnoreCase)
        {
            "handover",
            "in_progress",
            "ready",
            "cancelled"
        };

        private readonly IStringingOrder _stringingBL;

        public StringingController(IStringingOrder stringingBL)
        {
            _stringingBL = stringingBL;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetAll() => Ok(_stringingBL.GetAll());

        [HttpGet("my")]
        [Authorize]
        public IActionResult GetMyOrders()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            return Ok(_stringingBL.GetByUserId(userId.Value));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(StringingOrderCreateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var created = _stringingBL.Create(model, userId.Value);
            if (created == null) return NotFound("User not found");

            return Ok(created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest("Status is required.");
            }

            if (!AllowedStatuses.Contains(status))
            {
                return BadRequest($"Invalid status. Allowed statuses: {string.Join(", ", AllowedStatuses)}");
            }

            var order = _stringingBL.UpdateStatus(id, status);
            if (order == null) return NotFound();
            return Ok(order);
        }

        private int? GetCurrentUserId()
        {
            var rawId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(rawId, out var userId) ? userId : null;
        }
    }
}
