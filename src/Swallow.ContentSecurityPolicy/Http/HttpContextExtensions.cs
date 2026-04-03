using Microsoft.AspNetCore.Http;

namespace Swallow.ContentSecurityPolicy.Http;

public static class HttpContextExtensions
{
    extension(HttpContext context)
    {
        public ContentSecurityPolicyFeature? ContentSecurityPolicy => context.Features.Get<ContentSecurityPolicyFeature>();
    }
}
