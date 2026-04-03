namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class TrustedTypesEval : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<StyleSourceDirective>
{
    public static readonly TrustedTypesEval Instance = new();

    public override string Value => "'trusted-types-eval'";
}
