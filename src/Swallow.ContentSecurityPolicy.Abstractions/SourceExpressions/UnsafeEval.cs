using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UnsafeEval : SourceExpression, IAppliesTo<DefaultSourceDirective>
{
    public static readonly UnsafeEval Instance = new();

    public override string Value => "'unsafe-eval'";
}
