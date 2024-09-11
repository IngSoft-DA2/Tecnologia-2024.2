using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace shop.WebApi.Controllers;
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "T-shirt", Description = "A comfortable cotton T-shirt", Price = 19.99, CreatedAt = DateTime.UtcNow },
            new Product { Id = 2, Name = "Jeans", Description = "Classic blue jeans", Price = 39.99, CreatedAt = DateTime.UtcNow },
            new Product { Id = 3, Name = "Sneakers", Description = "Running shoes with extra cushioning", Price = 59.99, CreatedAt = DateTime.UtcNow }
        };

        [HttpGet]
        public IActionResult Get([FromQuery] string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                return Ok(result);
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            product.CreatedAt = DateTime.UtcNow;
            products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            products.Clear();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            products.Remove(product);
            return NoContent();
        }
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
}