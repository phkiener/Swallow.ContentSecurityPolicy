using Microsoft.Extensions.DependencyInjection;
using Swallow.ContentSecurityPolicy.Http;

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
    /// Set a default policy for the <see cref="ContentSecurityPolicyFeature"/>.
    /// </summary>
    /// <param name="policy">The policy to apply.</param>
    public ContentSecurityPolicyConfiguration SetDefaultPolicy(Abstractions.ContentSecurityPolicy policy)
    {
        serviceCollection.Configure<ContentSecurityPolicyOptions>(opt => opt.DefaultPolicy = policy);
        return this;
    }
}
