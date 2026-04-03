using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class DenyAll : SourceExpression, IAppliesTo<DefaultSourceDirective>
{
    public static readonly DenyAll Instance = new();

    public override string Value => "'none'";
}
