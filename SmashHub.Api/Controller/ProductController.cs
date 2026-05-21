using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

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
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = _productBL.Create(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, Product updated)

        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = _productBL.Update(id, updated);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (!_productBL.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
