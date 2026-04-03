namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class HostSource(string hostString) : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ChildSourceDirective>,
    IAppliesTo<ConnectSourceDirective>,
    IAppliesTo<FontSourceDirective>,
    IAppliesTo<FormActionDirective>
{
    public override string Value { get; } = hostString;
}
