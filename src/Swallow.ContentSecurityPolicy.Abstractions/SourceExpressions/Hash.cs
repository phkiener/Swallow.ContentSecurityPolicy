namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class Hash(Hash.Algorithm algorithm, string hashedValue) : FetchSourceExpression
{
    public Hash(Algorithm algorithm, byte[] hash) : this(algorithm, Convert.ToBase64String(hash)) { }

    public enum Algorithm { SHA256, SHA384, SHA512 }

    public Algorithm UsedAlgorithm { get; } = algorithm;
    public string HashedValue { get; } = hashedValue;

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
