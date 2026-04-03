using System.Collections.Frozen;

namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class Nonce : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<StyleSourceDirective>
{
    private static readonly FrozenSet<char> Base64Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToFrozenSet();
    private static readonly FrozenSet<char> Base64UrlAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_".ToFrozenSet();

    public Nonce()
    {
        NonceValue = GenerateNonce();
    }

    public Nonce(string nonce)
    {
        ValidateNonce(nonce);

        NonceValue = nonce;
    }

    public string NonceValue { get; }

    public override string Value => $"'nonce-{NonceValue}'";

    private static void ValidateNonce(string nonce)
    {
        var isValid = nonce.All(Base64Alphabet.Contains) || nonce.All(Base64UrlAlphabet.Contains);
        if (!isValid)
        {
            throw new FormatException("The given nonce is invalid; is must either use Base64 or URL-safe Base64.");
        }
    }

    private static string GenerateNonce() => Guid.NewGuid().ToString("D");
}
