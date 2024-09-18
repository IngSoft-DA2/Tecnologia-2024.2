using FluentAssertions;
using System.Text;
using shop.WebApi.Controllers;
using shop.WebApi.Services;
using shop.WebApi.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace shop.WebApi.UnitTests
{
    [TestClass]
    public sealed class FiltersServerTest
    {
        private TestServer _server;
        private HttpClient _client;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddControllers(options =>
                        {
                            options.Filters.Add(typeof(CustomExceptionFilter));
                        })
                        .AddApplicationPart(typeof(ProductsController).Assembly);

                    services.AddSingleton<IProductService, ProductService>();
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            
            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        #region Create
        
        #region Handled Error
        [TestMethod]
        public void Create_WhenProductIsNull_ShouldThrowException()
        {
            // Arrange
            StringContent content = new StringContent("{}", Encoding.UTF8, "application/json");

            // Act
            var response = _client.PostAsync("products", content).Result;

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        #endregion
        
        #endregion
    }
}
