using Microsoft.AspNetCore.Mvc;
using SmashHub.Api.Domain;

namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Product> _products = new List<Product>();

        [HttpGet]
        public IActionResult GetAll() => Ok(_products);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Title))
                return BadRequest("Title is required");

            if (product.Title.Length < 3 || product.Title.Length > 100)
                return BadRequest("Title must be between 3 and 100 characters");

            if (product.Price <= 0)
                return BadRequest("Price must be greater than 0");

            if (product.Price > 999999)
                return BadRequest("Price is too high");

            if (string.IsNullOrWhiteSpace(product.Description))
                return BadRequest("Description is required");

            if (product.Description.Length > 1000)
                return BadRequest("Description must not exceed 1000 characters");

            if (string.IsNullOrWhiteSpace(product.Category))
                return BadRequest("Category is required");

            if (string.IsNullOrWhiteSpace(product.Condition))
                return BadRequest("Condition is required");

            if (string.IsNullOrWhiteSpace(product.Image))
                return BadRequest("Image is required");

            _products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            if (string.IsNullOrWhiteSpace(updatedProduct.Title))
                return BadRequest("Title is required");

            if (updatedProduct.Title.Length < 3 || updatedProduct.Title.Length > 100)
                return BadRequest("Title must be between 3 and 100 characters");

            if (updatedProduct.Price <= 0)
                return BadRequest("Price must be greater than 0");

            if (updatedProduct.Price > 999999)
                return BadRequest("Price is too high");

            if (string.IsNullOrWhiteSpace(updatedProduct.Description))
                return BadRequest("Description is required");

            if (updatedProduct.Description.Length > 1000)
                return BadRequest("Description must not exceed 1000 characters");

            if (string.IsNullOrWhiteSpace(updatedProduct.Category))
                return BadRequest("Category is required");

            if (string.IsNullOrWhiteSpace(updatedProduct.Condition))
                return BadRequest("Condition is required");

            if (string.IsNullOrWhiteSpace(updatedProduct.Image))
                return BadRequest("Image is required");

            product.Title = updatedProduct.Title;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;
            product.Category = updatedProduct.Category;
            product.Condition = updatedProduct.Condition;
            product.Image = updatedProduct.Image;

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            _products.Remove(product);
            return NoContent();
        }
    }
}