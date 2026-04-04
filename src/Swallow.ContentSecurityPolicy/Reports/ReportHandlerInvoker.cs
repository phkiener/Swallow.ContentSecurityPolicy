using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Swallow.ContentSecurityPolicy.Reports;

internal sealed class ReportHandlerInvoker(ILogger<ReportHandlerInvoker> logger, IEnumerable<IReportHandler> handlers)
{
    private readonly IReportHandler[] handlers = handlers.ToArray();

    private async Task Invoke(HttpContext context)
    {
        if (handlers is [])
        {
            logger.LogError("CSP violation report handling was specified but no {Interface} was registered.", nameof(IReportHandler));

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        if (context.Request.ContentType is "application/reports+json")
        {
            var jsonElement = await context.Request.ReadFromJsonAsync<JsonElement>(context.RequestAborted);
            var violations = jsonElement.ValueKind is JsonValueKind.Array
                ? jsonElement.Deserialize(CspViolationReportSourceGenerationContext.Default.CSPViolationReportArray)
                : [jsonElement.Deserialize(CspViolationReportSourceGenerationContext.Default.CSPViolationReport)];

            if (violations is not (null or []))
            {
                foreach (var handler in handlers)
                {
                    await handler.Handle(violations, context.RequestAborted);
                }
            }
        }
    }

    public static Task Delegate(HttpContext context)
    {
        var reportHandler = context.RequestServices.GetRequiredService<ReportHandlerInvoker>();
        return reportHandler.Invoke(context);
    }
}
