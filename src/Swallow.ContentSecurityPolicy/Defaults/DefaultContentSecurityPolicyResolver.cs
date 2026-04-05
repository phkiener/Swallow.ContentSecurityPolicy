using Microsoft.Extensions.Options;
using Swallow.ContentSecurityPolicy.Abstractions;

namespace Swallow.ContentSecurityPolicy.Defaults;

/// <inheritdoc />
/// <remarks>
/// This resolver is basically a wrapper around the <see cref="ContentSecurityPolicyOptions"/>.
/// </remarks>
/// <param name="options">The configured <see cref="ContentSecurityPolicyOptions"/>.</param>
public sealed class DefaultContentSecurityPolicyResolver(IOptions<ContentSecurityPolicyOptions> options) : IContentSecurityPolicyResolver
{
    /// <inheritdoc />
    public ContentSecurityPolicyDefinition? FallbackPolicy()
    {
        return options.Value.FallbackPolicy;
    }

    /// <inheritdoc />
    public ContentSecurityPolicyDefinition? DefaultPolicy()
    {
        return options.Value.DefaultPolicy;
    }

    /// <inheritdoc />
    public ContentSecurityPolicyDefinition? GetPolicy(string name)
    {
        return options.Value.GetPolicy(name);
    }
}
