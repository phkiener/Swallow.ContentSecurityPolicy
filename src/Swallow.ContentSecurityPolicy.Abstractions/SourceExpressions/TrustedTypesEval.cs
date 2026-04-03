using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow unsafe evaluation using trusted types.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#trusted-types-eval">trusted-types-eval on MDN</seealso>
public sealed class TrustedTypesEval : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    /// <summary>
    /// A shared instance of the <see cref="TrustedTypesEval"/> expression.
    /// </summary>
    public static readonly TrustedTypesEval Instance = new();

    /// <inheritdoc />
    public override string Value => "'trusted-types-eval'";
}
