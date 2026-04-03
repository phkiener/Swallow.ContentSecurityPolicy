using System.Text.Json;
using Swallow.ContentSecurityPolicy;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning)
    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information)
    .AddFilter("Swallow.ContentSecurityPolicy", LogLevel.Trace);

var policy = new ContentSecurityPolicy { DefaultSource = [new Nonce()], StyleSource = [new Nonce()] };
builder.Services.AddContentSecurityPolicy().SetDefaultPolicy(policy);

var options = new JsonSerializerOptions { WriteIndented = true };
var app = builder.Build();

app.UseContentSecurityPolicy();
app.MapGet("/", ctx => Results.Json(ctx.ContentSecurityPolicy, options: options).ExecuteAsync(ctx));

app.Run();
