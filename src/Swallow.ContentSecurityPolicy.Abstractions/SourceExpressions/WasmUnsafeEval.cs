namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class WasmUnsafeEval : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    public static readonly WasmUnsafeEval Instance = new();

    public override string Value => "'wasm-unsafe-eval'";
}
