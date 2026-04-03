using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to include a sample in reports.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#report-sample">report-sample on MDN</seealso>
public sealed class ReportSample : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceAttributeDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceAttributeDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    /// <summary>
    /// A shared instance of the <see cref="ReportSample"/> expression.
    /// </summary>
    public static readonly ReportSample Instance = new();

    /// <inheritdoc />
    public override string Value => "'report-sample'";
}
