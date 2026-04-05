using Microsoft.Extensions.Options;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Defaults;

namespace Swallow.ContentSecurityPolicy.Tests.Defaults;

public sealed class DefaultContentSecurityPolicyResolverTest
{
    private readonly ContentSecurityPolicyOptions options = new();

    private DefaultContentSecurityPolicyResolver Resolver => new(new OptionsWrapper<ContentSecurityPolicyOptions>(options));

    [Test]
    public async Task ReturnsNull_WhenNoDefaultPolicyPresent()
    {
        await Assert.That(Resolver.DefaultPolicy()).IsNull();
    }

    [Test]
    public async Task ReturnsNull_WhenNoNamedPolicyPresent()
    {
        await Assert.That(Resolver.GetPolicy("Foobar")).IsNull();
    }

    [Test]
    public async Task ReturnsDefaultPolicy_WhenConfigured()
    {
        options.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Nothing));

        var policy = await Assert.That(Resolver.DefaultPolicy()).IsNotNull();
        await Assert.That(policy.DefaultSource?.Expressions).HasSingleItem(e => e.Equals(Allow.Nothing));
    }

    [Test]
    public async Task ReturnsNamedPolicy_WhenConfigured()
    {
        options.AddPolicy("Foobar", b => b.AddDefaultSource(Allow.Nothing));

        var policy = await Assert.That(Resolver.GetPolicy("Foobar")).IsNotNull();
        await Assert.That(policy.DefaultSource?.Expressions).HasSingleItem(e => e.Equals(Allow.Nothing));
    }

    [Test]
    public async Task ReturnsNull_WhenNamedPolicyHasDifferentName()
    {
        options.AddPolicy("Foobar", b => b.AddDefaultSource(Allow.Nothing));
        await Assert.That(Resolver.GetPolicy("not foobar")).IsNull();
    }
}
