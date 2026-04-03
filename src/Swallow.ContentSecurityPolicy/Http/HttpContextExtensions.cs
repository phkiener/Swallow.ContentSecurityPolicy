using Microsoft.AspNetCore.Http;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Http;

/// <summary>
/// Extensions for the <see cref="HttpContext"/> for convenience.
/// </summary>
public static class HttpContextExtensions
{
    extension(HttpContext context)
    {
        /// <summary>
        /// Return the current <see cref="Abstractions.ContentSecurityPolicy"/>.
        /// </summary>
        public Abstractions.ContentSecurityPolicy? ContentSecurityPolicy => context.Features.Get<ContentSecurityPolicyFeature>()?.Policy;

        /// <summary>
        /// Return the nonce that will be used for all <see cref="Nonce"/> expressions.
        /// </summary>
        public string? CspNonce => context.Features.Get<ContentSecurityPolicyFeature>()?.Nonce;
    }
}
