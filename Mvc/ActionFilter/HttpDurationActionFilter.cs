using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mvc.ActionFilter;

public class HttpDurationActionFilter(ILogger<HttpDurationActionFilter> logger) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Items["startTime"] = DateTime.Now;
        var method = context.HttpContext.Request.Method;
        var path = context.HttpContext.Request.Path;
        var parameters = await ReadBody(context.HttpContext);

        logger.LogInformation($"Begin {method} Path:{path} parameters:{parameters}");

        // 補充說明 給 Result 他就不能執行 Action 了
        // context.Result = new ContentResult(){Content = "Hello"};
        await next();

        var duration = DateTime.Now - (DateTime)context.HttpContext.Items["startTime"]!;
        ((Controller)context.Controller).ViewBag.Duration = duration;

        logger.LogInformation($"End {method} cost:{duration}");
    }

    private static async Task<string> ReadBody(HttpContext context)
    {
        context.Request.EnableBuffering();
        var text = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        var parameters = string.IsNullOrWhiteSpace(text)
            ? new Dictionary<string, object>()
            : JsonSerializer.Deserialize<Dictionary<string, object>>(text) ?? new Dictionary<string, object>();
         
        return string.Join(",", parameters.Select(r => $"{r.Key}={r.Value}"));
    }
}