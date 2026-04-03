namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UnsafeInline : FetchSourceExpression
{
    public static readonly UnsafeInline Instance = new();

    public override string Value => "'unsafe-inline'";
}
