using DemoHost;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Http.HttpResults;
using Swallow.ContentSecurityPolicy;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Endpoints;
using Swallow.ContentSecurityPolicy.Abstractions.Feature;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents();
builder.Logging.SetMinimumLevel(LogLevel.Warning)
    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information)
    .AddFilter("Swallow.ContentSecurityPolicy", LogLevel.Trace);

builder.Services.AddContentSecurityPolicyReportHandler<ReportHandler>();
builder.Services.AddContentSecurityPolicy(static opt =>
{
    // The fallback policy - used for all endpoints that do not specify a policy.
    opt.SetFallbackPolicy(static b => b
        .AddDefaultSource(Allow.Nothing)
        .SendReportsToLocal());

    // The default policy - used when no specific policy is referred to.
    opt.SetFallbackPolicy(static b => b
        .AddDefaultSource(Allow.Nothing)
        .SendReportsToLocal());

    // A specific policy.
    opt.AddPolicy("Locked Down", b => b.AddDefaultSource(Allow.Nothing));
});


var app = builder.Build();
StaticWebAssetsLoader.UseStaticWebAssets(app.Environment, app.Configuration);
app.MapStaticAssets();
app.UseHttpsRedirection();

// Send the response header
app.UseContentSecurityPolicy();

// Provide an endpoint to handle reports (SendReportsToLocal automatically uses this endpoint)
app.MapContentSecurityPolicyViolations();

// Uses the default policy because no name is specified.
app.MapGet("/", () => new RazorComponentResult<IndexPage>())
    .WithContentSecurityPolicy();

// Uses the fallback policy because no CSP is specified.
app.MapGet("/nonce", ctx => ctx.Response.WriteAsync($"The nonce is '{ctx.Nonce}'"));

// Uses a specific policy.
app.MapGet("/locked-down", ctx => ctx.Response.WriteAsync("You can't load anything!"))
    .WithContentSecurityPolicy("Locked Down");

// Skips the CSP entirely.
app.MapGet("/ignored", ctx => ctx.Response.WriteAsync("I don't have a CSP."))
    .DisableContentSecurityPolicy();

app.Run();
