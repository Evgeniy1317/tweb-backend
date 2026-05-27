using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic.Interfaces;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICart _cartBL;

        public CartController(ICart cartBL)
        {
            _cartBL = cartBL;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            return Ok(_cartBL.GetByUserId(userId.Value));
        }

        [HttpPost("{productId}")]
        public IActionResult Add(int productId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var cartItem = _cartBL.Add(userId.Value, productId);
            if (cartItem == null) return NotFound("Product not found");

            return Ok(cartItem);
        }

        [HttpDelete("{productId}")]
        public IActionResult Remove(int productId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            if (!_cartBL.Remove(userId.Value, productId)) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Clear()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            _cartBL.Clear(userId.Value);
            return NoContent();
        }

        private int? GetCurrentUserId()
        {
            var rawId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(rawId, out var userId) ? userId : null;
        }
    }
}
