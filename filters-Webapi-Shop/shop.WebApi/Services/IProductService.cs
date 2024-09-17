
using shop.WebApi.Models;

namespace shop.WebApi.Services
{
    public interface IProductService
    {
        Product Add(Product product);

        Product GetById(int id);

        List<Product> GetAll();

        void UpdateById(int id, string? description = null);

        void DeleteById(int id);
    }
}
