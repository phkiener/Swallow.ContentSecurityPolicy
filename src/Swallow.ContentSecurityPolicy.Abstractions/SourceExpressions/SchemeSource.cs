namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class SchemeSource(string scheme) : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ChildSourceDirective>
{
    public string Scheme { get; } = scheme;

    public override string Value => $"{Scheme}:";
}
