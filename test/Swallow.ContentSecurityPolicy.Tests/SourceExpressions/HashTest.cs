using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Tests.SourceExpressions;

public sealed class HashTest
{
    [Test]
    public async Task EncodesBytes_IsBase64()
    {
        const string hexedHash = "a591a6d40bf420404a011733cfb7b190d62c65bf0bcda32b57b277d9ad9f146e";
        const string base64 = "pZGm1Av0IEBKARczz7exkNYsZb8LzaMrV7J32a2fFG4=";
        var bytes = Convert.FromHexString(hexedHash);

        var fromBytes = new Hash(Hash.Algorithm.SHA256, bytes);
        var fromString = new Hash(Hash.Algorithm.SHA256, base64);

        await Assert.That(fromBytes).IsEqualTo(fromString);
    }

    [Test]
    public async Task RendersAllAlgorithms()
    {
        using (Assert.Multiple())
        {
            const string sha256Hash = "pZGm1Av0IEBKARczz7exkNYsZb8LzaMrV7J32a2fFG4=";
            await Assert.That(new Hash(Hash.Algorithm.SHA256, sha256Hash).Value).IsEqualTo($"'sha256-{sha256Hash}'");

            const string sha384Hash = "mVFDKRhrL2rkoTKefubGEKcpY2M1F0rGt0D5AoOW/MgD0Ok4Y6fD2Q+Gvu54L08/";
            await Assert.That(new Hash(Hash.Algorithm.SHA384, sha384Hash).Value).IsEqualTo($"'sha384-{sha384Hash}'");

            const string sha512Hash = "LHT9F+2v2A6ER7DUZ0HuJDt+t03SFJoKsbkkb7MDgvJ+hT2FhXGeDmfL2g2qj1FnEGRhXWRa4nrLFb+xRH9Fmw==";
            await Assert.That(new Hash(Hash.Algorithm.SHA512, sha512Hash).Value).IsEqualTo($"'sha512-{sha512Hash}'");
        }
    }
}
