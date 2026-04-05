using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swallow.ContentSecurityPolicy.Abstractions.Reports;

namespace Swallow.ContentSecurityPolicy.Reports;

/// <summary>
/// The endpoint for handling content security policy violations.
/// </summary>
public static class ViolationReportHandler
{
    /// <summary>
    /// Name of the endpoint, used for e.g. the <see cref="LinkGenerator"/>.
    /// </summary>
    public const string EndpointName = "csp-violation-report";

    internal sealed class Invoker(IEnumerable<IReportHandler> reportHandlers, ILogger<Invoker> logger)
    {
        private readonly List<IReportHandler> reportHandlers = reportHandlers.ToList();

        public async Task InvokeAsync(HttpContext context, CancellationToken cancellationToken)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;

            if (context.Request.ContentType is not "application/reports+json")
            {
                context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                context.Response.Headers.Accept = "application/reports+json";
                return;
            }

            try
            {
                var jsonElement = await context.Request.ReadFromJsonAsync<JsonElement>(context.RequestAborted);
                var violations = jsonElement.ValueKind is JsonValueKind.Array
                    ? jsonElement.Deserialize(ViolationReportSourceGenerationContext.Default.ViolationReportArray)
                    : [jsonElement.Deserialize(ViolationReportSourceGenerationContext.Default.ViolationReport)];

                if (reportHandlers is [])
                {
                    logger.LogWarning("CSP violations are being handled, but no {Type} is registered.", nameof(IReportHandler));
                    return;
                }

                if (violations is not (null or []))
                {
                    foreach (var handler in reportHandlers)
                    {
                        try
                        {
                            await handler.Handle(violations, cancellationToken);
                        }
                        catch (Exception e)
                        {
                            logger.LogError(e, "Unhandled exception in CSP report handler");
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        }
                    }
                }
            }
            catch (JsonException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }

    internal static Task Delegate(HttpContext context)
    {
        var invoker = context.RequestServices.GetRequiredService<Invoker>();
        return invoker.InvokeAsync(context, context.RequestAborted);
    }
}
