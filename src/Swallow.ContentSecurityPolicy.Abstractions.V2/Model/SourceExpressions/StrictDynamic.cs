using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow trusted - by a <see cref="Nonce"/> or <see cref="Hash"/> - scripts to load further scripts
/// on their own, without needing them to be trusted directly.
/// </summary>
/// <remarks>
/// Setting this expression essentially removes all <see cref="HostSource"/>, <see cref="SchemeSource"/>, <see cref="Self"/> and <see cref="UnsafeInline"/>
/// expressions.
/// </remarks>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#strict-dynamic">strict-dynamic on MDN</seealso>
public sealed record StrictDynamic :
    ISourceExpression<DefaultSourceDirective>,
    ISourceExpression<ScriptSourceDirective>,
    ISourceExpression<ScriptSourceElementDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceElementDirective>;
