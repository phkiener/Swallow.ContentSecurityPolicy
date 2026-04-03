using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Swallow.ContentSecurityPolicy.Configuration;
using Swallow.ContentSecurityPolicy.Internal;

namespace Swallow.ContentSecurityPolicy.Http;

public sealed class ContentSecurityPolicyMiddleware(IOptions<ContentSecurityPolicyOptions> options) : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var defaultPolicy = options.Value.DefaultPolicy;
        if (defaultPolicy is not null)
        {
            context.Response.OnStarting(WriteHeader, context);

            var feature = new ContentSecurityPolicyFeature(defaultPolicy);
            context.Features.Set(feature);
        }

        return next(context);
    }

    private static Task WriteHeader(object parameter)
    {
        if (parameter is not HttpContext context)
        {
            return Task.CompletedTask;
        }

        var feature = context.ContentSecurityPolicy;
        if (feature?.Current is { } policy)
        {
            HeaderValueWriter.SetHeader(context.Response.Headers, policy);
        }

        return Task.CompletedTask;
    }
}
