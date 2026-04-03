namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

/// <summary>
/// The <c>upgrade-insecure-requests</c> directive.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/upgrade-insecure-requests">upgrade-insecure-requests on MDN</seealso>
public sealed class UpgradeInsecureRequestsDirective() : Directive(Name)
{
    /// <summary>
    /// Name of the directive.
    /// </summary>
    public new const string Name = "upgrade-insecure-requests";

    /// <inheritdoc />
    public override IEnumerable<ISourceExpression> Expressions { get; } = [];
}
