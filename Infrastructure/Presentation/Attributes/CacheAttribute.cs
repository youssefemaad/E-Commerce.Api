using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Presentation;

class CacheAttribute(int durationInSecond = 90) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string CacheKey = CreateCacheKey(context.HttpContext.Request);
        ICachingService cachingService = context.HttpContext.RequestServices.GetRequiredService<ICachingService>();
        var CacheValue = await cachingService.GetAsync(CacheKey);
        if (CacheValue is not null)
        {
            context.Result = new ContentResult
            {
                Content = CacheValue,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }
        var ExecutedContext = await next.Invoke();
        if (ExecutedContext.Result is ObjectResult result)
        {
            await cachingService.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(durationInSecond));
        }
    }

    private string CreateCacheKey(HttpRequest request)
    {
        StringBuilder Key = new StringBuilder();
        Key.Append(request.Path + "?");
        foreach (var item in request.Query.OrderBy(o => o.Key))
        {
            Key.Append($"{item.Key}={item.Value}&");
        }
        return Key.ToString();
    }

}
