namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class Nonce : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    public override string Value => "'nonce-{0}'";
}
