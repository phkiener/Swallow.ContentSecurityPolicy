namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class HostSource(string hostString) : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ChildSourceDirective>,
    IAppliesTo<ConnectSourceDirective>
{
    public override string Value { get; } = hostString;
}
