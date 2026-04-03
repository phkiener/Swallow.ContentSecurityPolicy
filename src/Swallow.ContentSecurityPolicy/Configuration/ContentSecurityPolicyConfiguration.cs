using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Swallow.ContentSecurityPolicy.Http;
using Swallow.ContentSecurityPolicy.Reports;

namespace Swallow.ContentSecurityPolicy.Configuration;

/// <summary>
/// Configure the CSP setup.
/// </summary>
public sealed class ContentSecurityPolicyConfiguration
{
    private readonly IServiceCollection serviceCollection;

    internal ContentSecurityPolicyConfiguration(IServiceCollection serviceCollection)
    {
        this.serviceCollection = serviceCollection;
    }

    /// <summary>
    /// Set the initial policy for the <see cref="ContentSecurityPolicyFeature"/>.
    /// </summary>
    /// <param name="policy">The policy to apply.</param>
    public ContentSecurityPolicyConfiguration SetPolicy(Abstractions.ContentSecurityPolicy policy)
    {
        serviceCollection.Configure<ContentSecurityPolicyOptions>(opt => opt.DefaultPolicy = policy);
        return this;
    }

    /// <summary>
    /// Use the local reporting endpoint for the policy specified by <see cref="SetPolicy"/>.
    /// </summary>
    /// <typeparam name="THandler">The <see cref="IReportHandler"/> used to handle the report.</typeparam>
    /// <remarks>Use <see cref="Setup.MapContentSecurityPolicyReports"/> to setup the endpoint for handling the reports.</remarks>
    public ContentSecurityPolicyConfiguration UseReportHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>()
        where THandler : class, IReportHandler
    {
        serviceCollection.TryAddScoped<IReportHandler, THandler>();
        serviceCollection.AddSingleton<IConfigureOptions<ContentSecurityPolicyOptions>, SetReportingEndpoint>();

        return this;
    }
}
