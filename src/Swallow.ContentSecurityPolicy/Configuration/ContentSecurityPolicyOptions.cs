using Swallow.ContentSecurityPolicy.Http;

namespace Swallow.ContentSecurityPolicy.Configuration;

/// <summary>
/// Configured options for the <see cref="ContentSecurityPolicyFeature"/>.
/// </summary>
public sealed class ContentSecurityPolicyOptions
{
    /// <summary>
    /// The default policy to apply.
    /// </summary>
    public Abstractions.ContentSecurityPolicy? DefaultPolicy { get; set; }
}
