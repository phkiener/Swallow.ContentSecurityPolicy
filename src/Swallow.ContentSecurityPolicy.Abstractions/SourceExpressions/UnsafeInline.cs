namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UnsafeInline : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceAttributeDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceAttributeDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    public static readonly UnsafeInline Instance = new();

    public override string Value => "'unsafe-inline'";
}
