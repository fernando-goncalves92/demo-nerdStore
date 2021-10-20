using Microsoft.AspNetCore.Http;
using NerdStore.WebApp.MVC.Exceptions;
using System.Net;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpResponseException error)
            {
                if (error.StatusCode == HttpStatusCode.Unauthorized)
                {
                    httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                    
                    return;
                }

                httpContext.Response.StatusCode = (int)error.StatusCode;
            }
        }
    }
}
