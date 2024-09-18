using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace shop.WebApi.Common
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;
        
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(
                context.Exception, "Exception occurred: {Message}", context.Exception.Message);

            var error = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = context.Exception.Message
            };

            context.Result = new ObjectResult(error);
        }
    }
}