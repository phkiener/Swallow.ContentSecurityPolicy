using Microsoft.AspNetCore.Http;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2.Feature;

/// <summary>
/// Extensions on the <see cref="HttpContext"/> related to the content security policy.
/// </summary>
public static class HttpContextExtensions
{
    extension(HttpContext context)
    {
        /// <summary>
        /// Return the <see cref="IContentSecurityPolicyFeature"/> if it was set up on the <see cref="HttpContext"/>.
        /// </summary>
        public IContentSecurityPolicyFeature? ContentSecurityPolicyFeature => context.Features.Get<IContentSecurityPolicyFeature>();

        /// <summary>
        /// The nonce for the content security policy.
        /// </summary>
        public string? Nonce => context.ContentSecurityPolicyFeature?.Nonce;

        /// <summary>
        /// The <see cref="ContentSecurityPolicyDefinition"/> that applies to the current response, if any.
        /// </summary>
        public ContentSecurityPolicyDefinition? ContentSecurityPolicy => context.ContentSecurityPolicyFeature?.Policy;
    }
}
