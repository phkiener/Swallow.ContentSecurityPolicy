namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UnsafeHashes : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<StyleSourceDirective>
{
    public static readonly UnsafeHashes Instance = new();

    public override string Value => "'unsafe-hashes'";
}
