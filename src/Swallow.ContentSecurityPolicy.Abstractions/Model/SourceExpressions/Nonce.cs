using Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow resources that have a given nonce.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#nonce-nonce_value">nonce on MDN</seealso>
public sealed record Nonce :
    ISourceExpression<DefaultSourceDirective>,
    ISourceExpression<ScriptSourceDirective>,
    ISourceExpression<ScriptSourceElementDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceElementDirective>;
