using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Swallow.ContentSecurityPolicy.Configuration;

namespace Swallow.ContentSecurityPolicy.Http;

/// <summary>
/// A middleware to set a <see cref="Abstractions.ContentSecurityPolicy"/> in the response headers.
/// </summary>
/// <param name="options">The configured options.</param>
/// <param name="nonceGenerator">The <see cref="INonceGenerator"/> used to pre-fill the nonce.</param>
/// <remarks>
/// Use <c>HttpContext.ContentSecurityPolicy</c> and <c>HttpContext.CspNonce</c> to access the CSP and the nonce, respectively.
/// The CSP header is written in <see cref="HttpResponse.OnStarting(Func{Task})"/> so that it may be modified by further
/// middlewares or the endpoint itself.
/// </remarks>
public sealed class ContentSecurityPolicyMiddleware(IOptions<ContentSecurityPolicyOptions> options, INonceGenerator nonceGenerator) : IMiddleware
{
    /// <inheritdoc />
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.OnStarting(WriteHeader, context);

        var feature = new ContentSecurityPolicyFeature(nonceGenerator.Generate());
        context.Features.Set(feature);

        var defaultPolicy = options.Value.DefaultPolicy;
        if (defaultPolicy is not null)
        {
            feature.Current = defaultPolicy;
        }

        return next(context);
    }

    private static Task WriteHeader(object parameter)
    {
        if (parameter is not HttpContext context)
        {
            return Task.CompletedTask;
        }

        var feature = context.Features.Get<ContentSecurityPolicyFeature>();
        feature?.SetHeader(context.Response.Headers);

        return Task.CompletedTask;
    }
}
