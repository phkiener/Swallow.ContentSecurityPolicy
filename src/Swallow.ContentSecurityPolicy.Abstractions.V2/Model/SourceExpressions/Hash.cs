using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow resources with the given hash.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#hash_algorithm-hash_value">hash on MDN</seealso>
/// <param name="HashAlgorithm">The <see cref="Algorithm"/> that was used.</param>
/// <param name="HashedValue">The base64-encoded hash.</param>
public sealed record Hash(Hash.Algorithm HashAlgorithm, string HashedValue) :
    ISourceExpression<DefaultSourceDirective>,
    ISourceExpression<ScriptSourceDirective>,
    ISourceExpression<ScriptSourceElementDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceElementDirective>
{
    /// <summary>
    /// Set the containing <see cref="Directive"/> to allow resources with the given hash.
    /// </summary>
    /// <param name="algorithm">The <see cref="Algorithm"/> used.</param>
    /// <param name="hash">The hashed value.</param>
    public Hash(Algorithm algorithm, byte[] hash) : this(algorithm, Convert.ToBase64String(hash)) { }

    /// <summary>
    /// The algorithms supported by the <see cref="Hash"/> expression.
    /// </summary>
    public enum Algorithm
    {
        /// <summary>
        /// SHA256
        /// </summary>
        SHA256,

        /// <summary>
        /// SHA384
        /// </summary>
        SHA384,

        /// <summary>
        /// SHA512
        /// </summary>
        SHA512
    }
}
