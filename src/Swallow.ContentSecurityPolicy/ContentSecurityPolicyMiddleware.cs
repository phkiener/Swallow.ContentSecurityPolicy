using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Swallow.ContentSecurityPolicy.Internal;

namespace Swallow.ContentSecurityPolicy;

public sealed class ContentSecurityPolicyMiddleware(IOptions<ContentSecurityPolicyOptions> options) : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var defaultPolicy = options.Value.DefaultPolicy;
        if (defaultPolicy is not null)
        {
            HeaderValueWriter.SetHeader(context.Response.Headers, defaultPolicy);

            var feature = new ContentSecurityPolicyFeature(defaultPolicy);
            context.Features.Set(feature);
        }

        return next(context);
    }
}
