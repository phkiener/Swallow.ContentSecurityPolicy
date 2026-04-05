using Microsoft.AspNetCore.Builder;

namespace Swallow.ContentSecurityPolicy.Abstractions.Endpoints;

/// <summary>
/// Extensions to add CSP-related metadata to an endpoint.
/// </summary>
public static class EndpointConventionBuilderExtensions
{
    extension(IEndpointConventionBuilder endpoint)
    {
        /// <summary>
        /// Apply a <see cref="IgnoreContentSecurityPolicyAttribute"/>, causing it
        /// to ignore the default content security policy.
        /// </summary>
        public IEndpointConventionBuilder DisableContentSecurityPolicy()
        {
            return endpoint.WithMetadata(new IgnoreContentSecurityPolicyAttribute());
        }

        /// <summary>
        /// Apply a <see cref="ContentSecurityPolicyAttribute"/>, ensuring that the endpoint uses a
        /// content security policy.
        /// </summary>
        /// <param name="name">
        /// Name of the content security policy to use or <see langword="null"/> if the default
        /// policy should be used.
        /// </param>
        public IEndpointConventionBuilder WithContentSecurityPolicy(string? name = null)
        {
            return endpoint.WithMetadata(new ContentSecurityPolicyAttribute(name));
        }
    }
}
