using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swallow.ContentSecurityPolicy.Abstractions.V2;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Reports;
using Swallow.ContentSecurityPolicy.V2.Defaults;
using Swallow.ContentSecurityPolicy.V2.Internal;
using Swallow.ContentSecurityPolicy.V2.Reports;

namespace Swallow.ContentSecurityPolicy.V2;

/// <summary>
/// Register the content security policy features in a host.
/// </summary>
public static class ServiceProviderConfig
{
    /// <summary>
    /// Add the required services to the given <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> in which to register the services.</param>
    /// <param name="configure">An action to configure the <see cref="ContentSecurityPolicyOptions"/>.</param>
    public static IServiceCollection AddContentSecurityPolicy(this IServiceCollection services, Action<ContentSecurityPolicyOptions>? configure = null)
    {
        services.AddSingleton<ServicesRegisteredMarker>();
        services.AddScoped<ViolationReportHandler.Invoker>();
        services.AddScoped<ContentSecurityPolicyMiddleware>();

        services.TryAddScoped<IContentSecurityPolicyHeaderWriter, DefaultContentSecurityPolicyHeaderWriter>();
        services.TryAddScoped<IContentSecurityPolicyResolver, DefaultContentSecurityPolicyResolver>();
        services.TryAddScoped<IContentSecurityPolicyNonceGenerator, DefaultContentSecurityPolicyNonceGenerator>();

        services.AddOptions<ContentSecurityPolicyOptions>()
            .Configure(configure ?? (static _ => { }));

        return services;
    }

    /// <summary>
    /// Adds the given type <typeparamref name="T"/> as a <see cref="IReportHandler"/>.
    /// </summary>
    /// <remarks>
    /// This method is provided purely for convenience; it is no different to calling
    /// <code><![CDATA[services.AddScoped<IReportHandler, T>();]]></code>
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> in which to register the report handler.</param>
    /// <typeparam name="T">Type of the report handler.</typeparam>
    public static IServiceCollection AddContentSecurityPolicyReportHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        this IServiceCollection services) where T : class, IReportHandler
    {
        services.AddScoped<IReportHandler, T>();

        return services;
    }

    /// <summary>
    /// Use the content security policy middleware, setting a <see cref="ContentSecurityPolicyDefinition"/> if applicable.
    /// </summary>
    /// <remarks>
    /// This requires <see cref="AddContentSecurityPolicy"/> to have been called.
    /// </remarks>
    /// <param name="applicationBuilder">The host to which to add the middleware.</param>
    public static void UseContentSecurityPolicy(this IApplicationBuilder applicationBuilder)
    {
        if (applicationBuilder.ApplicationServices.GetService<ServicesRegisteredMarker>() is null)
        {
            throw new InvalidOperationException($"Required services for the content security policy have not been registered; make sure to call builder.Services.{nameof(AddContentSecurityPolicy)}().");
        }

        applicationBuilder.UseMiddleware<ContentSecurityPolicyMiddleware>();
    }

    /// <summary>
    /// Adds an endpoint to handle content security policy violation reports.
    /// </summary>
    /// <remarks>
    /// <p>
    /// The handled reports will be passed to <em>all</em> registered <see cref="IReportHandler"/>s.
    /// </p>
    /// <p>
    /// This endpoint will be used when e.g. <see cref="ContentSecurityPolicyBuilder.SendReportsToLocal()"/> was called,
    /// i.e. a policy is set to handle violations "locally".
    /// </p>
    /// <p>
    /// You can also register your own endpoint instead; to have it be picked up, be sure to use
    /// <see cref="ViolationReportHandler.EndpointName"/> as endpoint name.
    /// </p>
    /// </remarks>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the endpoint.</param>
    /// <param name="route">The route to use for the endpoint.</param>
    public static IEndpointConventionBuilder MapContentSecurityPolicyViolations(
        this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string route = "_framework/content-security-policy/violations")
    {
        return endpoints.MapPost(route, ViolationReportHandler.Delegate)
            .WithName(ViolationReportHandler.EndpointName)
            .WithDisplayName("Content Security Policy violation report handler")
            .AllowAnonymous()
            .DisableAntiforgery();
    }

    private sealed class ServicesRegisteredMarker;
}
