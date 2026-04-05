using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swallow.ContentSecurityPolicy.Abstractions.V2;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Feature;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Metadata;
using Swallow.ContentSecurityPolicy.V2.Reports;

namespace Swallow.ContentSecurityPolicy.V2.Internal;

/// <summary>
/// A middleware that registers the <see cref="IContentSecurityPolicyFeature"/> on the <see cref="HttpContext"/>
/// and ensures that the content security policy is written to the response - if applicable.
/// </summary>
/// <remarks>
/// The <see cref="IContentSecurityPolicyFeature"/> is <em>only</em> registered if a <see cref="ContentSecurityPolicyDefinition"/>
/// was resolved for the current endpoint.
/// </remarks>
public sealed class ContentSecurityPolicyMiddleware(
    IContentSecurityPolicyResolver policyResolver,
    IContentSecurityPolicyHeaderWriter headerWriter,
    IContentSecurityPolicyNonceGenerator nonceGenerator,
    LinkGenerator linkGenerator) : IMiddleware
{
    /// <inheritdoc/>
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var ignoreContentSecurityPolicy = context.GetEndpoint()?.Metadata.GetMetadata<IIgnoreContentSecurityPolicy>();
        if (ignoreContentSecurityPolicy is not null)
        {
            return next(context);
        }

        var specificContentSecurityPolicy = context.GetEndpoint()?.Metadata.GetMetadata<IContentSecurityPolicyData>();
        var policy = specificContentSecurityPolicy is null ? policyResolver.DefaultPolicy() : policyResolver.GetPolicy(specificContentSecurityPolicy.Name);
        if (policy is null)
        {
            // TODO: Log warning when policy is defined but not found (?)
            return next(context);
        }

        var nonce = nonceGenerator.Generate(context);
        var feature = new ContentSecurityPolicyFeature(context, linkGenerator, headerWriter, nonce, policy);

        context.Features.Set<IContentSecurityPolicyFeature>(feature);
        context.Response.OnStarting(OnStartingCallback, state: feature);

        return next(context);
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
        ContentSecurityPolicyDefinition policy) : IContentSecurityPolicyFeature
    {
        public void WriteHeaders()
        {
            var reportingUri = linkGenerator.GetPathByName(
                httpContext: httpContext,
                pathBase: httpContext.Request.PathBase,
                endpointName: ViolationReportHandler.EndpointName);

            var writerContext = new ContentSecurityPolicyWriterContext(Nonce, reportingUri);
            headerWriter.WriteTo(httpContext.Response.Headers, Policy, writerContext);
        }

        public string Nonce { get; } = nonce;
        public ContentSecurityPolicyDefinition Policy { get; } = policy;
    }
}
