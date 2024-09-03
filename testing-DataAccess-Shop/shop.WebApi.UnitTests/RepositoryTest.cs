using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using shop.WebApi.Models;
using shop.WebApi.DataAccess.Repositories;

namespace shop.WebApi.UnitTests
{
    [TestClass]
    public class RepositoryTest
    {
        private readonly ProductTestDbContext _context;
        private readonly Repository<Product> _repository;

        public RepositoryTest()
        {
            _context = DbContextBuilder.BuildTestDbContext();

            _repository = new Repository<Product>(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        #region Add
        #region Success
        [TestMethod]
        public void Add_WhenInfoIsProvided_ShouldAddedToDatabase()
        {
            var product = new Product();

            _repository.Add(product);

            using var otherContext = DbContextBuilder.BuildTestDbContext();

            var entitiesSaved = otherContext.EntitiesTest.ToList();

            entitiesSaved.Count.Should().Be(1);

            var savedProduct = entitiesSaved[0];

            savedProduct.Id.Should().Be(product.Id);
            savedProduct.Name.Should().Be(product.Name);
        }
        #endregion
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAll_WhenExistOnlyOne_ShouldReturnOne()
        {
            var expectedProduct = new Product
            {
                Name = "dummy"
            };
            using var context = DbContextBuilder.BuildTestDbContext();
            context.Add(expectedProduct);
            context.SaveChanges();

            var savedProducts = _repository.GetAll();

            savedProducts.Count.Should().Be(1);

            var savedProduct = savedProducts[0];
            
            savedProduct.Id.Should().Be(expectedProduct.Id);
            savedProduct.Name.Should().Be(expectedProduct.Name);
        }
        #endregion
    }

    internal sealed class ProductTestDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> EntitiesTest { get; set; }
    }
}
