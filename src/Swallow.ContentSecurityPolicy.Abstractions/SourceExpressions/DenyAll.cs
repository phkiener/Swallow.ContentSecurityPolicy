namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class DenyAll : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ChildSourceDirective>,
    IAppliesTo<ConnectSourceDirective>
{
    public static readonly DenyAll Instance = new();

    public override string Value => "'none'";
}
