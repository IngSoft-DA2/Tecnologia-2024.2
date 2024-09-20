using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using starwars.Exceptions.BusinessLogicExceptions;

namespace starwars.WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
    public void OnException(ExceptionContext context)
        {
                // En un proyecto real, se agrega logging mas sofisticado
                Console.WriteLine($"Message: {e.Message} - StackTrace: {e.StackTrace}");

                context.Result = new ObjectResult(new
                        { Message = "We encountered some issues, try again later" })
                        { StatusCode = 500 };
        }
    }
}

