using Microsoft.AspNetCore.Http;

namespace Swallow.ContentSecurityPolicy;

public static class HttpContextExtensions
{
    extension(HttpContext context)
    {
        public ContentSecurityPolicyFeature? ContentSecurityPolicy => context.Features.Get<ContentSecurityPolicyFeature>();
    }
}
