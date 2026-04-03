namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class InlineSpeculationRules : FetchSourceExpression
{
    public static readonly InlineSpeculationRules Instance = new();

    public override string Value => "'inline-speculation-rules'";
}
