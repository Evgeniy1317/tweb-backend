using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain.Models.Product;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productBL;

        public ProductController(IProduct productBL)
        {
            _productBL = productBL;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_productBL.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productBL.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ProductCreateModel product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!HasValidImageCount(product.Image, product.ExtraImages)) return BadRequest("A product can have up to 8 images.");

            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var created = _productBL.Create(product, userId.Value);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, ProductUpdateModel updated)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!HasValidImageCount(updated.Image, updated.ExtraImages)) return BadRequest("A product can have up to 8 images.");

            var existing = _productBL.GetById(id);
            if (existing == null) return NotFound();
            if (!CanManageProduct(existing.OwnerId)) return Forbid();

            var product = _productBL.Update(id, updated);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var existing = _productBL.GetById(id);
            if (existing == null) return NotFound();
            if (!CanManageProduct(existing.OwnerId)) return Forbid();

            if (!_productBL.Delete(id)) return NotFound();
            return NoContent();
        }

        private bool CanManageProduct(int? ownerId)
        {
            if (User.IsInRole("Admin")) return true;

            var userId = GetCurrentUserId();
            return userId != null && ownerId == userId.Value;
        }

        private int? GetCurrentUserId()
        {
            var rawId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(rawId, out var userId) ? userId : null;
        }

        private static bool HasValidImageCount(string image, List<string>? extraImages)
        {
            var imageCount = string.IsNullOrWhiteSpace(image) ? 0 : 1;
            imageCount += extraImages?.Count(extraImage => !string.IsNullOrWhiteSpace(extraImage)) ?? 0;
            return imageCount <= 8;
        }
    }
}
