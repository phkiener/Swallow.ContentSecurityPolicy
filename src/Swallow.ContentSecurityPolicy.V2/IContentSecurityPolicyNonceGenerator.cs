using Microsoft.AspNetCore.Http;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.V2;

/// <summary>
/// Generates a nonce to use for <see cref="Nonce"/> expressions.
/// </summary>
public interface IContentSecurityPolicyNonceGenerator
{
    /// <summary>
    /// Generates a nonce for a request.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/> of the request for which to generate a nonce.</param>
    /// <returns>The generated nonce.</returns>
    public string Generate(HttpContext httpContext);
}
