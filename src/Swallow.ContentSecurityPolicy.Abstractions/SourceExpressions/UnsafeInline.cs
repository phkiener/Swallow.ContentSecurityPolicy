using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UnsafeInline : SourceExpression, IAppliesTo<DefaultSourceDirective>
{
    public static readonly UnsafeInline Instance = new();

    public override string Value => "'unsafe-inline'";
}
