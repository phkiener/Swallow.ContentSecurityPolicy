using Swallow.ContentSecurityPolicy;

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
app.Run();
