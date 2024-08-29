using Microsoft.AspNetCore.Mvc;
using shop.WebApi.Services;
using shop.WebApi.Models;

namespace shop.WebApi.Controllers
{
    [ApiController]
    [Route("products")]
    public sealed class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            if (product == null)
            {
                throw new Exception("Product can not be null");
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentNullException("The name can not be null or empty");
            }

            return _productService.Add(product);
        }

        [HttpGet]
        public List<Product> GetAll()
        {
           return _productService.GetAll();
        }

        [HttpGet("{id}")]
        public Product GetById(int id)
        {
            return _productService.GetById(id);
        }

        [HttpDelete("{id}")]
        public void DeleteById(int id)
        {
            _productService.DeleteById(id);
        }

        [HttpPut("{id}")]
        public void UpdateById(int id, string description)
        {
            if (description == null)
            {
                throw new Exception("The description can not be null");
            }

            _productService.UpdateById(id, description);
        }
    }
}
