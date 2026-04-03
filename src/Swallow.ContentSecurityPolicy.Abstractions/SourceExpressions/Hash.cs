using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow resources with the given hash.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#hash_algorithm-hash_value">hash on MDN</seealso>
public sealed class Hash(Hash.Algorithm algorithm, string hashedValue) : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>
{
    public Hash(Algorithm algorithm, byte[] hash) : this(algorithm, Convert.ToBase64String(hash)) { }

    /// <summary>
    /// The algorithms supported by the <see cref="Hash"/> expression.
    /// </summary>
    public enum Algorithm { SHA256, SHA384, SHA512 }

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
