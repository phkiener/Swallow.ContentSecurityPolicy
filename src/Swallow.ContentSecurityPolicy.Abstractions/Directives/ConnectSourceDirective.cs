namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

/// <summary>
/// The <c>connect-src</c> directive.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/connect-src">connect-src on MDN</seealso>
public sealed class ConnectSourceDirective() : FetchDirective<ConnectSourceDirective>(Name)
{
    /// <summary>
    /// Name of the directive.
    /// </summary>
    public new const string Name = "connect-src";
}
