using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to consider <see cref="Hash"/>es for inline JavaScript (like <c>onclick="..."</c>).
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#unsafe-hashes">unsafe-hashes on MDN</seealso>
public sealed class UnsafeHashes : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceAttributeDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceAttributeDirective>
{
    /// <summary>
    /// A shared instance of the <see cref="UnsafeHashes"/> expression.
    /// </summary>
    public static readonly UnsafeHashes Instance = new();

    /// <inheritdoc />
    public override string Value => "'unsafe-hashes'";
}
