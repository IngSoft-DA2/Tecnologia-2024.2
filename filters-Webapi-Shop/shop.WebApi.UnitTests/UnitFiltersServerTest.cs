using shop.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace shop.WebApi.UnitTests
{
    [TestClass]
    public sealed class UnitFiltersServerTest
    {
        #region Create

        #region Handled Error
        [TestMethod]
        public void OnException_InvokeException_ReturnsBadRequest()
        {
            // Arrange 
            var _logger = new Mock<ILogger<CustomExceptionFilter>>();;

            var customExceptionFilter = new CustomExceptionFilter(_logger.Object);

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext(httpContext, new(), new(), new());
            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new ArgumentNullException("The name can not be null or empty")
            };

            // Act
            customExceptionFilter.OnException(exceptionContext);

            // Assert
            Assert.IsInstanceOfType<ObjectResult>(exceptionContext.Result);
            var exception = exceptionContext.Result as ObjectResult;
            Assert.IsInstanceOfType<ProblemDetails>(exception!.Value);
            var message = exception.Value as ProblemDetails;
            Assert.AreEqual(400, message!.Status);
        }
        #endregion
        
        #endregion
    }
}
