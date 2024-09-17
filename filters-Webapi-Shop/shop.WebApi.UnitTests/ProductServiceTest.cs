using FluentAssertions;
using shop.WebApi.Models;
using shop.WebApi.Services;

namespace shop.WebApi.UnitTests
{
    [TestClass]
    public sealed class ProductServiceTest
    {
        private ProductService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new ProductService();
        }

        #region Create
        #region Error
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Create_WhenNameIsDuplicated_ShouldThrowException()
        {
            try
            {
                Product product = new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "Description 1",
                    Price = 10,
                    CreatedAt = DateTime.Now,
                };
                _service.Add(product);

                _service.Add(product);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Product duplicated");
                throw;
            }
        }
        #endregion

        #region Success
        #endregion
        #endregion
    }
}
