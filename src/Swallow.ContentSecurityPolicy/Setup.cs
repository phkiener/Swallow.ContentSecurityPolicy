using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Swallow.ContentSecurityPolicy;

public static class Setup
{
    public static IServiceCollection AddContentSecurityPolicy(this IServiceCollection services, Action<ContentSecurityPolicyOptions>? configure = null)
    {
        services.AddSingleton<ContentSecurityPolicyMarker>();
        services.AddOptions<ContentSecurityPolicyOptions>();

        if (configure is not null)
        {
            services.Configure(configure);
        }

        return services.AddTransient<ContentSecurityPolicyMiddleware>();
    }

    public static IApplicationBuilder UseContentSecurityPolicy(this IApplicationBuilder app)
    {
        if (app.ApplicationServices.GetService<ContentSecurityPolicyMarker>() is null)
        {
            throw new InvalidOperationException(
                $"Content Security Policy is not registered; this can be fixed by calling builder.Services.{nameof(AddContentSecurityPolicy)}()");
        }

        return app.UseMiddleware<ContentSecurityPolicyMiddleware>();
    }

    private sealed record ContentSecurityPolicyMarker;
}
