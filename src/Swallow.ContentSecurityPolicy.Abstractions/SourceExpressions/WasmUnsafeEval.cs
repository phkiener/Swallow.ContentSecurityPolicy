namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class WasmUnsafeEval : SourceExpression,
    IAppliesTo<DefaultSourceDirective>
{
    public static readonly WasmUnsafeEval Instance = new();

    public override string Value => "'wasm-unsafe-eval'";
}
