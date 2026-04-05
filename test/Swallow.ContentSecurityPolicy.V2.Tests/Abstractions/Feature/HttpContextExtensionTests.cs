using Swallow.ContentSecurityPolicy.Abstractions.V2;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Feature;

namespace Swallow.ContentSecurityPolicy.V2.Tests.Abstractions.Feature;

public sealed class HttpContextExtensionTests : EndToEndTestBase
{
    [Test]
    public async Task ContentSecurityPolicy_IsNullInHttpContext_WhenNoPolicyApplied()
    {
        ContentSecurityPolicyDefinition? observedPolicy = null;

        using var client = GetClient(
            services => services.AddContentSecurityPolicy(),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.Map("/policy", ctx =>
                {
                    observedPolicy = ctx.ContentSecurityPolicy;
                    return Task.CompletedTask;
                });
            });

        await client.GetAsync("/policy");
        await Assert.That(observedPolicy).IsNull();
    }

    [Test]
    public async Task DefaultPolicy_IsSetInHttpContext()
    {
        ContentSecurityPolicyDefinition? observedPolicy = null;
        var policy = new ContentSecurityPolicyBuilder().AddDefaultSource(Allow.Self).Build();

        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(policy)),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.Map("/policy", ctx =>
                {
                    observedPolicy = ctx.ContentSecurityPolicy;
                    return Task.CompletedTask;
                });
            });

        await client.GetAsync("/policy");
        await Assert.That(observedPolicy).IsSameReferenceAs(policy);
    }

    [Test]
    public async Task SpecificPolicy_IsSetInHttpContext()
    {
        ContentSecurityPolicyDefinition? observedPolicy = null;
        var defaultPolicy = new ContentSecurityPolicyBuilder().AddDefaultSource(Allow.Self).Build();
        var specificPolicy = new ContentSecurityPolicyBuilder().AddDefaultSource(Allow.Self).Build();

        using var client = GetClient(
            services => services.AddContentSecurityPolicy(
                opt => opt.SetDefaultPolicy(defaultPolicy)
                    .AddPolicy("Specific", specificPolicy)),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.Map("/policy", ctx =>
                {
                    observedPolicy = ctx.ContentSecurityPolicy;
                    return Task.CompletedTask;
                }).WithMetadata(new ContentSecurityPolicyAttribute("Specific"));
            });

        await client.GetAsync("/policy");
        await Assert.That(observedPolicy).IsSameReferenceAs(specificPolicy);
    }

    [Test]
    public async Task NonceIsNull_WhenNoPolicyApplied()
    {
        string? observedNonce = null;

        using var client = GetClient(
            services => services.AddContentSecurityPolicy(),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.Map("/policy", ctx =>
                {
                    observedNonce = ctx.Nonce;
                    return Task.CompletedTask;
                });
            });

        await client.GetAsync("/policy");
        await Assert.That(observedNonce).IsNull();
    }

    [Test]
    public async Task NonceIsSet_WhenPolicyApplied()
    {
        string? observedNonce = null;

        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self))),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.Map("/policy", ctx =>
                {
                    observedNonce = ctx.Nonce;
                    return Task.CompletedTask;
                });
            });

        await client.GetAsync("/policy");
        await Assert.That(observedNonce).IsNotNull();
    }
}
