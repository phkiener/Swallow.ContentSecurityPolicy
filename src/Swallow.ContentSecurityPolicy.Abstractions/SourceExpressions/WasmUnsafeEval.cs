using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow compiling WASM.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#wasm-unsafe-eval">wasm-unsafe-eval on MDN</seealso>
public sealed class WasmUnsafeEval : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    /// <summary>
    /// A shared instance of the <see cref="WasmUnsafeEval"/> expression.
    /// </summary>
    public static readonly WasmUnsafeEval Instance = new();

    /// <inheritdoc />
    public override string Value => "'wasm-unsafe-eval'";
}
