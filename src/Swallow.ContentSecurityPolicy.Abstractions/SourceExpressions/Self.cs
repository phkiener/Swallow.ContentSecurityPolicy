namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class Self : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ChildSourceDirective>,
    IAppliesTo<ConnectSourceDirective>,
    IAppliesTo<FontSourceDirective>
{
    public static readonly Self Instance = new();

    public override string Value => "'self'";
}
