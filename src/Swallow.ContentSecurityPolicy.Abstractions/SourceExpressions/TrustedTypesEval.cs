namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class TrustedTypesEval : FetchSourceExpression
{
    public static readonly TrustedTypesEval Instance = new();

    public override string Value => "'trusted-types-eval'";
}
