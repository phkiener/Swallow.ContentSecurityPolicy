namespace Swallow.ContentSecurityPolicy.Abstractions;

/// <summary>
/// A Content Security Policy that will control which external resources are fetched.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#directives">CSP on MDN</seealso>
public sealed partial class ContentSecurityPolicy
{
    /// <summary>
    /// The endpoint name to use when registering an endpoint to handle CSP violation reports.
    /// </summary>
    public const string ReportingEndpointName = "csp-reporting-endpoint";

    private readonly Dictionary<string, Directive> directives = [];

    /// <summary>
    /// The <see cref="Directive"/>s that this policy contains.
    /// </summary>
    public IEnumerable<Directive> Directives => directives.Values.AsEnumerable();

    /// <summary>
    /// When <see langword="true"/>, the policy will not actually block any resources, but will send a report.
    /// </summary>
    /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy-Report-Only">Content-Security-Policy-Report-Only on MDN</seealso>
    public bool ReportOnly { get; set; } = false;

    /// <summary>
    /// The endpoint to send CSP violation reports to.
    /// </summary>
    /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/report-to">report-to on MDN</seealso>
    public string? ReportingEndpoint { get; set; }

    private T? GetSpecific<T>(string key) where T : class
    {
        return directives.GetValueOrDefault(key) as T;
    }

    private void SetOrRemove(string key, Directive? value)
    {
        if (value is null)
        {
            directives.Remove(key);
        }
        else
        {
            directives[key] = value;
        }
    }
}
