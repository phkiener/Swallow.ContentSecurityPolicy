namespace Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

/// <summary>
/// The <c>frame-src</c> directive.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/frame-src">frame-src on MDN</seealso>
public sealed class FrameSourceDirective : FetchDirective<FrameSourceDirective>;
