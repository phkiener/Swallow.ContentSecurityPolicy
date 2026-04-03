using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow inline JavaScript.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#unsafe-inline">unsafe-inline on MDN</seealso>
public sealed class UnsafeInline : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceAttributeDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceAttributeDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    /// <summary>
    /// A shared instance of the <see cref="UnsafeInline"/> expression.
    /// </summary>
    public static readonly UnsafeInline Instance = new();

    /// <inheritdoc />
    public override string Value => "'unsafe-inline'";
}
