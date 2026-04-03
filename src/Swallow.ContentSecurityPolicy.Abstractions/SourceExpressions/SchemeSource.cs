namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class SchemeSource(string scheme) : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ChildSourceDirective>,
    IAppliesTo<ConnectSourceDirective>,
    IAppliesTo<FontSourceDirective>,
    IAppliesTo<FormActionDirective>
{
    public string Scheme { get; } = scheme;

    public override string Value => $"{Scheme}:";
}
