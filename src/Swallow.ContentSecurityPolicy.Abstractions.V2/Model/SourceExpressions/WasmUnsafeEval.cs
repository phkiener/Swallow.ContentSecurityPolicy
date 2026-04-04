using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow compiling WASM.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#wasm-unsafe-eval">wasm-unsafe-eval on MDN</seealso>
public sealed record WasmUnsafeEval :
    ISourceExpression<DefaultSourceDirective>,
    ISourceExpression<ScriptSourceDirective>,
    ISourceExpression<ScriptSourceElementDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceElementDirective>;
