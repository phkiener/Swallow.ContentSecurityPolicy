namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class Self : FetchSourceExpression
{
    public static readonly Self Instance = new();

    public override string Value => "'self'";
}
