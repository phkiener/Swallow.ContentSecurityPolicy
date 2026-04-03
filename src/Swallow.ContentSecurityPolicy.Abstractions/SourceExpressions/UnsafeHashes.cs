namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UnsafeHashes : FetchSourceExpression
{
    public static readonly UnsafeHashes Instance = new();

    public override string Value => "'unsafe-hashes'";
}
