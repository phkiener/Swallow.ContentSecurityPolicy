using Microsoft.AspNetCore.Http;

namespace Swallow.ContentSecurityPolicy.V2.Defaults;

/// <summary>
/// Generates a plain GUID as nonce.
/// </summary>
public sealed class DefaultContentSecurityPolicyNonceGenerator : IContentSecurityPolicyNonceGenerator
{
    /// <inheritdoc/>
    public string Generate(HttpContext httpContext)
    {
        return Guid.NewGuid().ToString("D");
    }
}
