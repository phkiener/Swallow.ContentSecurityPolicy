namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class TrustedTypesEval : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    public static readonly TrustedTypesEval Instance = new();

    public override string Value => "'trusted-types-eval'";
}
