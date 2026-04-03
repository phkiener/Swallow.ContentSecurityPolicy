using Swallow.ContentSecurityPolicy;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning)
    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information)
    .AddFilter("Swallow.ContentSecurityPolicy", LogLevel.Trace);

var policy = new ContentSecurityPolicy { DefaultSource = [new Nonce()], StyleSource = [new Nonce()] };
builder.Services.AddContentSecurityPolicy().SetDefaultPolicy(policy);

var app = builder.Build();

app.UseContentSecurityPolicy();
app.MapGet("/", ctx => ctx.Response.WriteAsync($"The nonce is '{ctx.CspNonce}'"));

app.Run();
