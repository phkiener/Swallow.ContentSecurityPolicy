using Microsoft.AspNetCore.Mvc.Testing;

namespace Swallow.ContentSecurityPolicy.Tests;

public abstract class EndToEndTestBase : IDisposable
{
    private WebApplicationFactory<Program>? webApplicationFactory;

    protected HttpClient GetClient(Action<IServiceCollection> configureServices, params Action<WebApplication>[] endpoints)
    {
        webApplicationFactory?.Dispose();
        webApplicationFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(b => b.ConfigureServices(services =>
            {
                configureServices(services);
                services.AddSingleton<Action<WebApplication>>(app =>
                {
                    foreach (var endpoint in endpoints)
                    {
                        endpoint(app);
                    }
                });
            }));

        webApplicationFactory.StartServer();
        return webApplicationFactory.CreateClient();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            webApplicationFactory?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
