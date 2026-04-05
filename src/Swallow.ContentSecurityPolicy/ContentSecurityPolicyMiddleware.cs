using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Endpoints;
using Swallow.ContentSecurityPolicy.Abstractions.Feature;
using Swallow.ContentSecurityPolicy.Reports;

namespace Swallow.ContentSecurityPolicy;

/// <summary>
/// A middleware that registers the <see cref="IContentSecurityPolicyFeature"/> on the <see cref="HttpContext"/>
/// and ensures that the content security policy is written to the response - if applicable.
/// </summary>
public sealed class ContentSecurityPolicyMiddleware(
    IContentSecurityPolicyResolver policyResolver,
    IContentSecurityPolicyHeaderWriter headerWriter,
    IContentSecurityPolicyNonceGenerator nonceGenerator,
    LinkGenerator linkGenerator) : IMiddleware
{
    /// <inheritdoc/>
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var policy = ResolvePolicy(context.GetEndpoint());

        var nonce = nonceGenerator.Generate(context);
        var feature = new ContentSecurityPolicyFeature(context, linkGenerator, headerWriter, nonce, policy);

        context.Features.Set<IContentSecurityPolicyFeature>(feature);
        context.Response.OnStarting(OnStartingCallback, state: feature);

        return next(context);
    }

    private ContentSecurityPolicyDefinition? ResolvePolicy(Endpoint? endpoint)
    {
        var ignorePolicy = endpoint?.Metadata.GetMetadata<IIgnoreContentSecurityPolicy>();
        if (ignorePolicy is not null)
        {
            return null;
        }

        var policyMetadata = endpoint?.Metadata.GetMetadata<IContentSecurityPolicyData>();
        if (policyMetadata?.Name is not null)
        {
            var policy = policyResolver.GetPolicy(policyMetadata.Name);
            return policy ?? throw new InvalidOperationException($"The content security policy {policyMetadata.Name} was configured for the endpoint, but couldn't be resolved.");
        }

        return policyMetadata is not null
            ? policyResolver.DefaultPolicy()
            : policyResolver.FallbackPolicy();
    }

    private static Task OnStartingCallback(object state)
    {
        var feature = (ContentSecurityPolicyFeature)state;
        feature.WriteHeaders();

        return Task.CompletedTask;
    }

    private sealed class ContentSecurityPolicyFeature(
        HttpContext httpContext,
        LinkGenerator linkGenerator,
        IContentSecurityPolicyHeaderWriter headerWriter,
        string nonce,
        ContentSecurityPolicyDefinition? policy) : IContentSecurityPolicyFeature
    {
        public void WriteHeaders()
        {
            if (Policy is null)
            {
                return;
            }

            var reportingUri = linkGenerator.GetPathByName(
                httpContext: httpContext,
                pathBase: httpContext.Request.PathBase,
                endpointName: ViolationReportHandler.EndpointName);

            var writerContext = new ContentSecurityPolicyWriterContext(Nonce, reportingUri);
            headerWriter.WriteTo(httpContext.Response.Headers, Policy, writerContext);
        }

        public string Nonce { get; } = nonce;
        public ContentSecurityPolicyDefinition? Policy { get; } = policy;
    }
}
