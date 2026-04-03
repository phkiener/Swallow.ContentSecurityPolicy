namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class StrictDynamic : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    public static readonly StrictDynamic Instance = new();

    public override string Value => "'strict-dynamic'";
}
