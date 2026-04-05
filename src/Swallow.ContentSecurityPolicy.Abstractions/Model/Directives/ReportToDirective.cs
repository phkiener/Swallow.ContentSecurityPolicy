namespace Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

/// <summary>
/// The <c>report-to</c> directive.
/// </summary>
/// <param name="url">The URL to which to report the violations to.</param>
/// <param name="endpointName">The name to use for the endpoint.</param>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/report-to">report-to on MDN</seealso>
public sealed class ReportToDirective(string url, string endpointName = "csp-reports") : Directive
{
    /// <summary>
    /// Marker URI which indicates that <see cref="Url"/> should be replaced with the Url to the
    /// local violation reports endpoint.
    /// </summary>
    public const string LocalEndpointUri = "<LOCAL>";

    /// <summary>
    /// The URL to which to report the violations to.
    /// </summary>
    /// <remarks></remarks>
    public string Url { get; } = url;

    /// <summary>
    /// The name to use for the endpoint.
    /// </summary>
    public string EndpointName { get; } = endpointName;
}
