namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UnsafeHashes : SourceExpression,
    IAppliesTo<DefaultSourceDirective>
{
    public static readonly UnsafeHashes Instance = new();

    public override string Value => "'unsafe-hashes'";
}
