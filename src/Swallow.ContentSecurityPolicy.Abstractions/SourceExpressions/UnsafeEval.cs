namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UnsafeEval : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<StyleSourceDirective>
{
    public static readonly UnsafeEval Instance = new();

    public override string Value => "'unsafe-eval'";
}
