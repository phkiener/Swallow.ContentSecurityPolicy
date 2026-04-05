using Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to load inline <c><![CDATA[<script>]]></c> elements with type <c>speculationrules</c>.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#inline-speculation-rules">inline-speculation-rules on MDN</seealso>
public sealed record InlineSpeculationRules :
    ISourceExpression<DefaultSourceDirective>,
    ISourceExpression<ScriptSourceDirective>,
    ISourceExpression<ScriptSourceElementDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceElementDirective>;
