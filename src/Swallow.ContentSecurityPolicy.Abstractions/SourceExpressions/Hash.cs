using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow resources with the given hash.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#hash_algorithm-hash_value">hash on MDN</seealso>
/// <param name="algorithm">The <see cref="Algorithm"/> used.</param>
/// <param name="hashedValue">The base64-encoded hash.</param>
public sealed class Hash(Hash.Algorithm algorithm, string hashedValue) : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
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

    /// <summary>
    /// The <see cref="Algorithm"/> used for this expression.
    /// </summary>
    public Algorithm UsedAlgorithm { get; } = algorithm;

    /// <summary>
    /// The hashed value.
    /// </summary>
    public string HashedValue { get; } = hashedValue;

    /// <inheritdoc />
    public override string Value => $"'{EncodeAlgorithm(UsedAlgorithm)}-{HashedValue}'";

    private static string EncodeAlgorithm(Algorithm algorithm)
    {
        return algorithm switch
        {
            Algorithm.SHA256 => "sha256",
            Algorithm.SHA384 => "sha384",
            Algorithm.SHA512 => "sha512",
            _ => throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null)
        };
    }
}
