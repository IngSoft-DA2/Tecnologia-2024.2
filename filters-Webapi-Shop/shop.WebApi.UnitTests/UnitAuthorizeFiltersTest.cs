using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace shop.WebApi.UnitTests
{
    [TestClass]
    public sealed class UnitAuthorizeFiltersTest
    {
        #region Authorize
        [TestMethod]
        public void OnAuthorization_SendHeader_Authorize()
        {
            // Arrange 
            var customAuthorizeFilter = new CustomAuthorizeFilterAttribute("Admin");

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "token";

            var actionContext = new ActionContext(httpContext, new(), new(), new());
            var authorizeContext = new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());

            // Act
            customAuthorizeFilter.OnAuthorization(authorizeContext);

            // Assert
            Assert.AreEqual(authorizeContext.ModelState.IsValid, true);
        }
        #endregion
    }
}
