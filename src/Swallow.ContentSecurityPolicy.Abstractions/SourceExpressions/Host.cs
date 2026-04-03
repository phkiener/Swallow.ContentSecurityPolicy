namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class HostSource(string hostString) : FetchSourceExpression
{
    public override string Value { get; } = hostString;
}
