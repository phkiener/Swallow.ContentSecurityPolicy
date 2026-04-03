using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow resources that have a given nonce.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#nonce-nonce_value">nonce on MDN</seealso>
public sealed class Nonce : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    /// <summary>
    /// A shared instance of the <see cref="Nonce"/> expression.
    /// </summary>
    public static readonly Nonce Instance = new();

    /// <inheritdoc />
    /// <remarks>
    /// Because the nonce is a <em>chaning</em> value, it is not present in this
    /// value. It is expected you pass the actual nonce to <see cref="string.Format(string, object)"/>:
    /// <code>
    /// string.Format(Nonce.Instance.Value, "my-nonce");
    /// </code>
    /// </remarks>
    public override string Value => "'nonce-{0}'";
}
