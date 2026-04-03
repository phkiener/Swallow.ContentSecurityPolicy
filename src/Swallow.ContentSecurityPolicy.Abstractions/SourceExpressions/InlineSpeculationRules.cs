namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class InlineSpeculationRules : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    public static readonly InlineSpeculationRules Instance = new();

    public override string Value => "'inline-speculation-rules'";
}
