using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy;

/// <summary>
/// Generate a nonce to use with <see cref="Nonce"/>.
/// </summary>
public interface INonceGenerator
{
    /// <summary>
    /// Return a valid nonce.
    /// </summary>
    /// <returns>The nonce to apply.</returns>
    /// <remarks>
    /// The nonce must be in either Base64 or URL-safe Base64.
    /// This is <em>not</em> checked.
    /// </remarks>
    string Generate();
}
