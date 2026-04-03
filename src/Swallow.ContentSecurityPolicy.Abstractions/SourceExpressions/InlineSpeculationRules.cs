using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to load inline <c><![CDATA[<script>]]></c> elements with type <c>speculationrules</c>.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#inline-speculation-rules">inline-speculation-rules on MDN</seealso>
public sealed class InlineSpeculationRules : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    /// <summary>
    /// A shared instance of the <see cref="InlineSpeculationRules"/> expression.
    /// </summary>
    public static readonly InlineSpeculationRules Instance = new();

    /// <inheritdoc />
    public override string Value => "'inline-speculation-rules'";
}
