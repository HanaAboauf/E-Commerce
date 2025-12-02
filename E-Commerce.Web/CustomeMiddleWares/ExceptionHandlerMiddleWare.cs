using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.CustomeMiddleWares
{
    /* this class consider as middleware if and only if  
     * it Containes a method with the name "Invoke" or "InvokeAsync" and has constractor that take RequestDelegate as parameter
     * and if it implements IMiddleware interface
     */
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ExceptionHandlerMiddleWare> _Logger;

        public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
        {
            _Next = next;
            _Logger = logger;
        }

        public async Task InvokAsync(HttpContext httpContext)
        {
            try
            {
                await _Next.Invoke(httpContext);
                await HandleNotFoundEndPoint(httpContext);

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Something went wrong");

                var problem = new ProblemDetails()
                {
                    Title = "Error while processing HTTP request",
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException=> StatusCodes.Status404NotFound,
                        _=> StatusCodes.Status500InternalServerError
                    }

                };
                httpContext.Response.StatusCode = problem.Status.Value;
               await httpContext.Response.WriteAsJsonAsync(problem);

            }
        }

        private static async Task HandleNotFoundEndPoint(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound && !httpContext.Response.HasStarted)
            {
                var problem = new ProblemDetails()
                {
                    Title = "Error while processing HTTP request -  endpoint doesn't found",
                    Detail = $"Endpoint: {httpContext.Request.Path} was not found on this server.",
                    Status = StatusCodes.Status404NotFound,
                    Instance = httpContext.Request.Path,
                };
                await httpContext.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
