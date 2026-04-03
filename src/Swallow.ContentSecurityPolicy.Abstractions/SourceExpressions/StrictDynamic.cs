using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class StrictDynamic : SourceExpression, IAppliesTo<DefaultSourceDirective>
{
    public static readonly StrictDynamic Instance = new();

    public override string Value => "'strict-dynamic'";
}
