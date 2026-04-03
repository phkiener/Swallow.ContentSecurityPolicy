using Swallow.ContentSecurityPolicy.Http;

namespace Swallow.ContentSecurityPolicy.Configuration;

/// <summary>
/// Configured options for the <see cref="ContentSecurityPolicyFeature"/>.
/// </summary>
public sealed class ContentSecurityPolicyOptions
{
    /// <summary>
    /// The default policy to apply.
    /// </summary>
    public Abstractions.ContentSecurityPolicy? DefaultPolicy { get; set; }

    /// <summary>
    /// Get the <em>endpoint name</em> to use when reporting is enabled.
    /// </summary>
    /// <remarks>
    /// This does not refer to the endpoint URI; the reporting endpoint will be used as key
    /// for the <c>Reporting-Endpoints</c> header.
    /// </remarks>
    public string ReportingEndpointName { get; set; } = "csp-reports";
}
