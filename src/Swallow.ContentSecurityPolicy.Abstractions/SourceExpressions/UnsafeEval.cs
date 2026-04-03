namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class UnsafeEval : FetchSourceExpression
{
    public static readonly UnsafeEval Instance = new();

    public override string Value => "'unsafe-eval'";
}
