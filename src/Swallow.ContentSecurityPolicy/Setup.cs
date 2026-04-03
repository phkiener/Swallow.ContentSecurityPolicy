using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swallow.ContentSecurityPolicy.Configuration;
using Swallow.ContentSecurityPolicy.Http;
using Swallow.ContentSecurityPolicy.Internal;
using Swallow.ContentSecurityPolicy.Reports;

namespace Swallow.ContentSecurityPolicy;

/// <summary>
/// Setup the Content Security Policy features.
/// </summary>
public static class Setup
{
    private const string CspNotRegistered =
        $"Content Security Policy is not registered; this can be fixed by calling builder.Services.{nameof(AddContentSecurityPolicy)}()";

    /// <summary>
    /// Register the required services in the given <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> in which to register the services.</param>
    /// <param name="configure">An action to configure the <see cref="ContentSecurityPolicyOptions"/>.</param>
    /// <returns>A <see cref="ContentSecurityPolicyConfiguration"/> to further configure the CSP options.</returns>
    public static ContentSecurityPolicyConfiguration AddContentSecurityPolicy(
        this IServiceCollection services,
        Action<ContentSecurityPolicyOptions>? configure = null)
    {
        services.AddSingleton<ContentSecurityPolicyMarker>();
        services.AddTransient(typeof(ReportHandlerInvoker));

        services.AddOptions<ContentSecurityPolicyOptions>();
        if (configure is not null)
        {
            services.Configure(configure);
        }

        services.TryAddScoped<INonceGenerator, GuidNonceGenerator>();
        services.AddTransient<ContentSecurityPolicyMiddleware>();

        return new ContentSecurityPolicyConfiguration(services);
    }

    /// <summary>
    /// Add a middleware to apply the configured content security policy.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to which to add the middleware.</param>
    /// <returns>The passed-in <see cref="IApplicationBuilder"/>.</returns>
    /// <exception cref="InvalidOperationException">When the required services are not registered using <see cref="AddContentSecurityPolicy"/>.</exception>
    public static IApplicationBuilder UseContentSecurityPolicy(this IApplicationBuilder app)
    {
        return app.ApplicationServices.GetService<ContentSecurityPolicyMarker>() is null
            ? throw new InvalidOperationException(CspNotRegistered)
            : app.UseMiddleware<ContentSecurityPolicyMiddleware>();
    }

    /// <summary>
    /// Add an endpoint to handle CSP violation reports.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the endpoint to.</param>
    /// <param name="route">The URL to use for the endpoint.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> to further configure the endpoint.</returns>
    /// <remarks>
    /// Use <see cref="ContentSecurityPolicyConfiguration.UseReportHandler{T}"/> to register an <see cref="IReportHandler"/> for this endpoint.
    /// </remarks>
    public static IEndpointConventionBuilder MapContentSecurityPolicyReports(this IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string route = "_csp/report")
    {
        return endpoints.MapPost(route, ReportHandlerInvoker.Delegate)
            .AllowAnonymous()
            .WithName(Abstractions.ContentSecurityPolicy.ReportingEndpointName)
            .WithDisplayName("Content Security Policy Reports");
    }

    internal sealed record ContentSecurityPolicyMarker;
}
