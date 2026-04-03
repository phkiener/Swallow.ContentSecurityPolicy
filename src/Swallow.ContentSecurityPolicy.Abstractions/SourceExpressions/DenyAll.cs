namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class DenyAll : FetchSourceExpression
{
    public static readonly DenyAll Instance = new();

    public override string Value => "'none'";
}
