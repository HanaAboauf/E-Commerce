using E_Commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    internal class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _Duration;

        public RedisCacheAttribute(int Duration = 5)
        {
            _Duration = Duration;
        }

        // we choose this function because this works before and after the endpoint is executed
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get cache service from DI container
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            //create cache key based on the request path and query string
            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            //check if the cached data exists or not
            var cachedData = await cacheService.GetAsync(cacheKey);
            // if exists, return the data from cache, and skip the execution of the endpoint
            if (cachedData is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cachedData,
                    ContentType = "application/Json",
                    StatusCode = StatusCodes.Status200OK

                };
                return;
            }

            var executedContext = await next.Invoke(); // execute the endpoint
            // if not exists, execute the endpoint and store the result in cache if it 200Ok
            if (executedContext.Result is OkObjectResult result)

                await cacheService.SetAsync(cacheKey, cachedData!, TimeSpan.FromMinutes(_Duration));



        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append($"{request.Path}");
            foreach (var (keyName, value) in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"|{keyName}-{value}");
            }
            return key.ToString();

        }
    }
}
