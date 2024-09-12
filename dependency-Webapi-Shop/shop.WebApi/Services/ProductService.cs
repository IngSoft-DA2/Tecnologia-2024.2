using shop.WebApi.Models;

namespace shop.WebApi.Services
{
    public class ProductService : IProductService, IDisposable
    {
        private static readonly List<Product> _products = [];

        public Product Add(Product product)
        {
            var existProduct = _products.Any(m => m.Name == product.Name);
            if (existProduct)
            {
                throw new Exception("Product duplicated");
            }

            var ProductToSave = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt,
            };

            _products.Add(ProductToSave);

            return ProductToSave;
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            Product? product = _products.FirstOrDefault(m => m.Id == id);

            if (product == null)
            {
                throw new Exception("Product dosen't exist");
            }

            return product;
        }

        public void DeleteById(int id)
        {
            Product product = GetById(id);

            _products.Remove(product);
        }

        public void UpdateById(int id, string? description = null)
        {
            Product productSaved = GetById(id);

            if (!string.IsNullOrEmpty(description))
            {
                productSaved.Description = description;
            }
        }

        public void Dispose() => _products.Clear();
    }
}
