using Microsoft.Extensions.DependencyInjection;

namespace Swallow.ContentSecurityPolicy.Configuration;

public sealed class ContentSecurityPolicyConfiguration
{
    private readonly IServiceCollection serviceCollection;

    internal ContentSecurityPolicyConfiguration(IServiceCollection serviceCollection)
    {
        this.serviceCollection = serviceCollection;
    }

    public ContentSecurityPolicyConfiguration SetDefaultPolicy(Abstractions.ContentSecurityPolicy policy)
    {
        serviceCollection.Configure<ContentSecurityPolicyOptions>(opt => opt.DefaultPolicy = policy);
        return this;
    }
}
