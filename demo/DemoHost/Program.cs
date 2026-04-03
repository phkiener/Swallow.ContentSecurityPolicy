using DemoHost;
using Swallow.ContentSecurityPolicy;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning)
    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information)
    .AddFilter("Swallow.ContentSecurityPolicy", LogLevel.Trace);

builder.Services.AddContentSecurityPolicy()
    .SetPolicy(new ContentSecurityPolicy { DefaultSource = [new Nonce()], StyleSource = [new Nonce()], ReportOnly = true })
    .UseReportHandler<ReportHandler>();

var app = builder.Build();

app.UseContentSecurityPolicy();
app.MapContentSecurityPolicyReports(route: "_framework/csp/handle-report");

app.MapGet("/", ctx => ctx.Response.WriteAsync($"The nonce is '{ctx.CspNonce}'"));
app.MapGet("/deny-all", ctx =>
{
    ctx.ContentSecurityPolicy?.DefaultSource = [DenyAll.Instance];
    return ctx.Response.WriteAsync("You can't load anything!");
});

app.Run();
