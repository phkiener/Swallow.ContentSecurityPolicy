namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

/// <summary>
/// The <c>default-src</c> directive.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/default-src">default-src on MDN</seealso>
public sealed class DefaultSourceDirective() : FetchDirective<DefaultSourceDirective>(Name)
{
    /// <summary>
    /// Name of the directive.
    /// </summary>
    public new const string Name = "default-src";
}
