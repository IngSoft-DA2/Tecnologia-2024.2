using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomAuthorizeFilterAttribute(string role) : Attribute,IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context != null)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (!String.IsNullOrEmpty(token))
            {
                //ToDo: Add the custom logic to validate the user
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedResult();
        }
    }
}