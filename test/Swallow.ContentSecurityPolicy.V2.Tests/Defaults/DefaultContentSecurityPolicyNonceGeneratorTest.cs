using Swallow.ContentSecurityPolicy.V2.Defaults;

namespace Swallow.ContentSecurityPolicy.V2.Tests.Defaults;

public sealed class DefaultContentSecurityPolicyNonceGeneratorTest
{
    private DefaultContentSecurityPolicyNonceGenerator Generator => new();

    [Test]
    public async Task GeneratesAGuidNonce()
    {
        var nonce = Generator.Generate(new DefaultHttpContext());
        await Assert.That(nonce).IsParsableInto<Guid>();
    }

    [Test]
    public async Task GeneratesDifferentNonces()
    {
        var context = new DefaultHttpContext();
        var nonces = Enumerable.Range(0, 10).Select(_ => Generator.Generate(context)).ToList();

        await Assert.That(nonces).HasDistinctItems();
    }
}
