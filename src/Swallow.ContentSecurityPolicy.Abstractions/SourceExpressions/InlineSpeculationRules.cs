namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class InlineSpeculationRules : SourceExpression,
    IAppliesTo<DefaultSourceDirective>
{
    public static readonly InlineSpeculationRules Instance = new();

    public override string Value => "'inline-speculation-rules'";
}
