namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class StrictDynamic : FetchSourceExpression
{
    public static readonly StrictDynamic Instance = new();

    public override string Value => "'strict-dynamic'";
}
