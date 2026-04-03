namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class Self : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ChildSourceDirective>,
    IAppliesTo<ConnectSourceDirective>
{
    public static readonly Self Instance = new();

    public override string Value => "'self'";
}
