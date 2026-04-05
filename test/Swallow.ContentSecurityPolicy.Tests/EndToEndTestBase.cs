using Microsoft.AspNetCore.Mvc.Testing;

namespace Swallow.ContentSecurityPolicy.Tests;

public abstract class EndToEndTestBase : IDisposable
{
    private WebApplicationFactory<Program>? webApplicationFactory;

    protected HttpClient GetClient(Action<IServiceCollection> configureServices, Action<WebApplication> configureApplication)
    {
        webApplicationFactory?.Dispose();
        webApplicationFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(b => b.ConfigureServices(services =>
            {
                configureServices(services);
                services.AddSingleton(configureApplication);
            }));

        webApplicationFactory.StartServer();
        return webApplicationFactory.CreateClient();
    }

    public void Dispose()
    {
        webApplicationFactory?.Dispose();
    }
}
