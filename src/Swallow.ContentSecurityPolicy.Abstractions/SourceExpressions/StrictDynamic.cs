using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow trusted - by a <see cref="Nonce"/> or <see cref="Hash"/> - scripts to load further scripts
/// on their own, without needing them to be trusted directly.
/// </summary>
/// <remarks>
/// Setting this expression essentially removes all <see cref="HostSource"/>, <see cref="SchemeSource"/>, <see cref="Self"/> and <see cref="UnsafeInline"/>
/// expressions.
/// </remarks>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#strict-dynamic">strict-dynamic on MDN</seealso>
public sealed class StrictDynamic : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    /// <summary>
    /// A shared instance of the <see cref="StrictDynamic"/> expression.
    /// </summary>
    public static readonly StrictDynamic Instance = new();

    /// <inheritdoc />
    public override string Value => "'strict-dynamic'";
}
