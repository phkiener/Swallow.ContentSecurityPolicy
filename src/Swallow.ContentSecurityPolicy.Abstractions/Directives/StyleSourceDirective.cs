namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

/// <summary>
/// The <c>style-src</c> directive.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/style-src">style-src on MDN</seealso>
public class StyleSourceDirective() : FetchDirective<StyleSourceDirective>(Name)
{
    /// <summary>
    /// Name of the directive.
    /// </summary>
    public new const string Name = "style-src";
}
