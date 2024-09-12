using Microsoft.AspNetCore.Mvc;
using shop.WebApi.DataAccess.Repositories;
using shop.WebApi.Models;

namespace shop.WebApi.Controllers
{
    [ApiController]
    [Route("products")]
    public sealed class ProductController(IRepository<Product> _productRepository) : ControllerBase
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

            return _productRepository.Add(product);
        }

        [HttpGet]
        public List<Product> GetAll()
        {
           return _productRepository.GetAll();
        }
    }
}
