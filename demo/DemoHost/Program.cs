using DemoHost;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Http.HttpResults;
using Swallow.ContentSecurityPolicy.Abstractions.V2;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Feature;
using Swallow.ContentSecurityPolicy.V2;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning)
    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information)
    .AddFilter("Swallow.ContentSecurityPolicy", LogLevel.Trace);

builder.Services.AddRazorComponents();
builder.Services.AddContentSecurityPolicy(static opt =>
{
    opt.SetDefaultPolicy(static b => b
        .AddDefaultSource(Allow.Self)
        .AddScriptSource(Allow.Nonce)
        .AddStyleSourceElement(Allow.UnsafeInline)
        .SendReportsToLocal());

    opt.AddPolicy("Locked Down", b => b.AddDefaultSource(Allow.Nothing));
});

builder.Services.AddContentSecurityPolicyReportHandler<ReportHandler>();

var app = builder.Build();

StaticWebAssetsLoader.UseStaticWebAssets(app.Environment, app.Configuration);
app.MapStaticAssets();
app.UseHttpsRedirection();

app.UseContentSecurityPolicy();
app.MapContentSecurityPolicyViolations(route: "content-security-policy/violations");

app.MapGet("/", () => new RazorComponentResult<IndexPage>());
app.MapGet("/nonce", ctx => ctx.Response.WriteAsync($"The nonce is '{ctx.Nonce}'"));

app.MapGet("/ignored", ctx => ctx.Response.WriteAsync("I don't have a CSP."))
    .WithMetadata(new IgnoreContentSecurityPolicyAttribute());

app.MapGet("/locked-down", ctx => ctx.Response.WriteAsync("You can't load anything!"))
    .WithMetadata(new ContentSecurityPolicyAttribute("Locked Down"));

app.Run();
