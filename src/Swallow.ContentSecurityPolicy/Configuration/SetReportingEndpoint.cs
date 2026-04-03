using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace Swallow.ContentSecurityPolicy.Configuration;

internal sealed class SetReportingEndpoint(LinkGenerator linkGenerator, IHttpContextAccessor? httpContextAccessor = null) : IConfigureOptions<ContentSecurityPolicyOptions>
{
    public void Configure(ContentSecurityPolicyOptions options)
    {
        options.Policy?.ReportingEndpoint ??= httpContextAccessor?.HttpContext is null
            ? linkGenerator.GetPathByName(Abstractions.ContentSecurityPolicy.ReportingEndpointName)
            : linkGenerator.GetPathByName(httpContextAccessor.HttpContext, Abstractions.ContentSecurityPolicy.ReportingEndpointName);
    }
}
