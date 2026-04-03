namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

/// <summary>
/// The <c>base-uri</c> directive.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/base-uri">base-uri on MDN</seealso>
public sealed class BaseUriDirective() : FetchDirective<BaseUriDirective>(Name)
{
    /// <summary>
    /// Name of the directive.
    /// </summary>
    public new const string Name = "base-uri";
}
