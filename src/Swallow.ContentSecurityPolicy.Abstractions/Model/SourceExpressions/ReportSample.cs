using Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to include a sample in reports.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#report-sample">report-sample on MDN</seealso>
public sealed record ReportSample :
    ISourceExpression<DefaultSourceDirective>,
    ISourceExpression<ScriptSourceDirective>,
    ISourceExpression<ScriptSourceAttributeDirective>,
    ISourceExpression<ScriptSourceElementDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceAttributeDirective>,
    ISourceExpression<StyleSourceElementDirective>;
