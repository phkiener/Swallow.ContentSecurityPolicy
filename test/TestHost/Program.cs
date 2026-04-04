using Swallow.ContentSecurityPolicy;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning)
    .AddFilter("Swallow.ContentSecurityPolicy", LogLevel.Information);

var app = builder.Build();

app.UseContentSecurityPolicy();

if (app.Configuration.GetValue<string>("CustomReportRoute") is { } route)
{
    app.MapContentSecurityPolicyReports(route);
}
else
{
    app.MapContentSecurityPolicyReports();
}

app.MapGet("/", () => "Hello World!");
app.MapGet("/nonce", ctx => ctx.Response.WriteAsync(ctx.CspNonce ?? ""));
app.MapGet("/unsafe-inline", ctx =>
{
    ctx.ContentSecurityPolicy?.DefaultSource = [UnsafeInline.Instance];
    return Task.CompletedTask;
});

app.Run();
