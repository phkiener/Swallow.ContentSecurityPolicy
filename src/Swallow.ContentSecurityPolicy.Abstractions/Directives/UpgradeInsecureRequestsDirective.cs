namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UpgradeInsecureRequestsDirective() : Directive(Name)
{
    public new const string Name = "upgrade-insecure-requests";
    public override IEnumerable<ISourceExpression> Expressions { get; } = [];
}
