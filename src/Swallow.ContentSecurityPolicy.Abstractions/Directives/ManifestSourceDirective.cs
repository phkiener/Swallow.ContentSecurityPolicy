namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

/// <summary>
/// The <c>manifest-src</c> directive.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/manifest-src">manifest-src on MDN</seealso>
public sealed class ManifestSourceDirective() : FetchDirective<ManifestSourceDirective>(Name)
{
    /// <summary>
    /// Name of the directive.
    /// </summary>
    public new const string Name = "manifest-src";
}
