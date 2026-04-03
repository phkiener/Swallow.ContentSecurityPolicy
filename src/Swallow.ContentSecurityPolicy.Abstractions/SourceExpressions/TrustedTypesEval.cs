namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class TrustedTypesEval : SourceExpression, IAppliesTo<DefaultSourceDirective>
{
    public static readonly TrustedTypesEval Instance = new();

    public override string Value => "'trusted-types-eval'";
}
