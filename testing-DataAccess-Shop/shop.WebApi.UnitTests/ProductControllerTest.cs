using FluentAssertions;
using Moq;
using shop.WebApi.Controllers;
using shop.WebApi.Models;
using shop.WebApi.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace shop.WebApi.UnitTests
{
    [TestClass]
    public sealed class ProductControllerTest
    {
        private Mock<IRepository<Product>> _repositoryMock;
        private ProductController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _repositoryMock = new Mock<IRepository<Product>>(MockBehavior.Strict);
            _controller = new ProductController(_repositoryMock.Object);
        }

        #region Create
        
        #region Error
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Create_WhenProductIsNull_ShouldThrowException()
        {
            _controller.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenProductHasNameNull_ShouldThrowException()
        {
            var product = new Product 
            { 
                Name = string.Empty,
                Description = "some description",
                CreatedAt = new DateTime()
            };

            _controller.Create(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenProductHasNameEmpty_ShouldThrowException()
        {
            var product = new Product
            {
                Name = string.Empty,
                Description = "some description",
                CreatedAt = new DateTime()
            };

            _controller.Create(product);
        }
        #endregion
        
        #region Success
        [TestMethod]
        public void Create_WhenProductHasCorrectInfo_ShouldCreateProduct()
        {
            Product product = new()
            {
                Id = 1,
                Name = "title",
                Description = "description",
                CreatedAt = new DateTime()
            };
            Product expectedProduct = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CreatedAt = product.CreatedAt
            };
            _repositoryMock.Setup(m => m.Add(It.IsAny<Product>())).Returns(expectedProduct);

            ActionResult<Product> response = _controller.Create(product);
            Product result = response!.Value;

            _repositoryMock.VerifyAll();
            result.Should().NotBeNull();
            result.Id!.Should().Be(expectedProduct.Id);
        }
        #endregion
        #endregion
    }
}
