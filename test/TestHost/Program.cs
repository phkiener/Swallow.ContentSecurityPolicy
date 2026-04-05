var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning)
    .AddFilter("Swallow.ContentSecurityPolicy", LogLevel.Information);

var app = builder.Build();

var appBuilder = app.Services.GetRequiredService<Action<WebApplication>>();
appBuilder(app);

app.Run();
