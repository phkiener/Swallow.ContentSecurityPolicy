using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

public sealed class ReportSample : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceAttributeDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceAttributeDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    public static readonly ReportSample Instance = new();

    public override string Value => "'report-sample'";
}
