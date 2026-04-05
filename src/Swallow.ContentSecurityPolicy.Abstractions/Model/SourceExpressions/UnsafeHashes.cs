using Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to consider <see cref="Hash"/>es for inline JavaScript (like <c>onclick="..."</c>).
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#unsafe-hashes">unsafe-hashes on MDN</seealso>
public sealed record UnsafeHashes :
    ISourceExpression<DefaultSourceDirective>,
    ISourceExpression<ScriptSourceDirective>,
    ISourceExpression<ScriptSourceAttributeDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceAttributeDirective>;
