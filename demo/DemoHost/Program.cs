using Swallow.ContentSecurityPolicy;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Directives;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning)
    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information)
    .AddFilter("Swallow.ContentSecurityPolicy", LogLevel.Trace);

builder.Services.AddContentSecurityPolicy(opt => opt.DefaultPolicy = new ContentSecurityPolicy
{
    DefaultSource = [new Nonce("schnitzel")]
});

var app = builder.Build();

app.UseContentSecurityPolicy();
app.MapGet("/", static ctx => ctx.Response.WriteAsync($"The nonce is '{ctx.ContentSecurityPolicy?.Nonce}'"));

app.Run();
