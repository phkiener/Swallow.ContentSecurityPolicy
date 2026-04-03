namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

/// <summary>
/// The <c>frame-ancestors</c> directive.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/frame-ancestors">frame-ancestors-uri on MDN</seealso>
public sealed class FrameAncestorsDirective() : FetchDirective<FrameAncestorsDirective>(Name)
{
    /// <summary>
    /// Name of the directive.
    /// </summary>
    public new const string Name = "frame-ancestors";
}
