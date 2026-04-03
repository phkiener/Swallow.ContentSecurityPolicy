using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swallow.ContentSecurityPolicy.Configuration;
using Swallow.ContentSecurityPolicy.Http;

namespace Swallow.ContentSecurityPolicy;

public static class Setup
{
    private const string CspNotRegistered =
        $"Content Security Policy is not registered; this can be fixed by calling builder.Services.{nameof(AddContentSecurityPolicy)}()";

    public static ContentSecurityPolicyConfiguration AddContentSecurityPolicy(
        this IServiceCollection services,
        Action<ContentSecurityPolicyOptions>? configure = null)
    {
        services.AddSingleton<ContentSecurityPolicyMarker>();
        services.AddOptions<ContentSecurityPolicyOptions>();
        services.AddTransient<ContentSecurityPolicyMiddleware>();

        if (configure is not null)
        {
            services.Configure(configure);
        }

        return new ContentSecurityPolicyConfiguration(services);
    }

    public static IApplicationBuilder UseContentSecurityPolicy(this IApplicationBuilder app)
    {
        return app.ApplicationServices.GetService<ContentSecurityPolicyMarker>() is null
            ? throw new InvalidOperationException(CspNotRegistered)
            : app.UseMiddleware<ContentSecurityPolicyMiddleware>();
    }

    private sealed record ContentSecurityPolicyMarker;
}
