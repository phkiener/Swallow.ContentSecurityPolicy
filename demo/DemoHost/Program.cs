using DemoHost;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Http.HttpResults;
using Swallow.ContentSecurityPolicy;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning)
    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information)
    .AddFilter("Swallow.ContentSecurityPolicy", LogLevel.Trace);

builder.Services.AddRazorComponents();
builder.Services.AddHttpContextAccessor();
builder.Services.AddContentSecurityPolicy()
    .SetPolicy(new ContentSecurityPolicy
    {
        DefaultSource = [Self.Instance],
        ScriptSource = [Nonce.Instance],
        StyleSourceElement = [UnsafeInline.Instance]
    })
    .UseReportHandler<ReportHandler>();

var app = builder.Build();

StaticWebAssetsLoader.UseStaticWebAssets(app.Environment, app.Configuration);
app.MapStaticAssets();

app.UseHttpsRedirection();
app.UseContentSecurityPolicy();
app.MapContentSecurityPolicyReports(route: "_framework/csp/handle-report");

app.MapGet("/", () => new RazorComponentResult<IndexPage>());
app.MapGet("/nonce", ctx => ctx.Response.WriteAsync($"The nonce is '{ctx.CspNonce}'"));
app.MapGet("/changed-policy", ctx =>
{
    ctx.ContentSecurityPolicy?.DefaultSource = [DenyAll.Instance];
    return ctx.Response.WriteAsync("You can't load anything!");
});


app.Run();
