namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class SchemeSource(string scheme) : FetchSourceExpression
{
    public string Scheme { get; } = scheme;

    public override string Value => $"{Scheme}:";
}
