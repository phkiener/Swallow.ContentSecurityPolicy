using Microsoft.AspNetCore.Mvc.Testing;

namespace Swallow.ContentSecurityPolicy.Tests.Framework;

public static class TestableHost
{
    public static WebApplicationFactory<Program> Start(
        Action<IServiceCollection>? configureServices = null,
        Action<IConfigurationBuilder>? configureConfiguration = null)
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(b =>
            {
                if (configureServices is not null)
                {
                    b.ConfigureServices(configureServices);
                }

                if (configureConfiguration is not null)
                {
                    b.ConfigureAppConfiguration(configureConfiguration);
                }
            });

        factory.StartServer();

        return factory;
    }
}
