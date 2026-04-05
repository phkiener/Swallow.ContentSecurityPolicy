using Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow unsafe evaluation using trusted types.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#trusted-types-eval">trusted-types-eval on MDN</seealso>
public sealed record TrustedTypesEval :
    ISourceExpression<DefaultSourceDirective>,
    ISourceExpression<ScriptSourceDirective>,
    ISourceExpression<ScriptSourceElementDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceElementDirective>;
