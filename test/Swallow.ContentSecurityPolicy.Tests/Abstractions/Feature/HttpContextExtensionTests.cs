using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Endpoints;
using Swallow.ContentSecurityPolicy.Abstractions.Feature;

namespace Swallow.ContentSecurityPolicy.Tests.Abstractions.Feature;

public sealed class HttpContextExtensionTests : EndToEndTestBase
{
    [Test]
    public async Task FeatureIsSet_WhenMiddlewareNotRegistered()
    {
        IContentSecurityPolicyFeature? feature = null;
        ContentSecurityPolicyDefinition? policy = null;
        string? nonce = null;

        using var client = GetClient(
            services => services.AddContentSecurityPolicy(),
            app => app.Map("/", ctx =>
            {
                feature = ctx.ContentSecurityPolicyFeature;
                policy = ctx.ContentSecurityPolicy;
                nonce = ctx.Nonce;

                return Task.CompletedTask;
            }));

        await client.GetAsync("/");
        await Assert.That(feature).IsNull();
        await Assert.That(policy).IsNull();
        await Assert.That(nonce).IsNull();
    }

    [Test]
    public async Task FeatureIsSet_EvenIfNoPolicyIsApplied()
    {
        IContentSecurityPolicyFeature? feature = null;
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(),
            app => app.UseContentSecurityPolicy(),
            app => app.Map("/", ctx => Observe(ctx, c => c.ContentSecurityPolicyFeature, out feature)));

        await client.GetAsync("/");
        await Assert.That(feature).IsNotNull();
        await Assert.That(feature.Nonce).IsNotNullOrEmpty();
        await Assert.That(feature.Policy).IsNull();
    }

    [Test]
    public async Task NonceIsSet_EvenIfNoPolicyApplied()
    {
        string? nonce = null;
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(),
            app => app.UseContentSecurityPolicy(),
            app => app.Map("/", ctx => Observe(ctx, c => c.Nonce, out nonce)));

        await client.GetAsync("/");
        await Assert.That(nonce).IsNotNullOrEmpty();
    }

    [Test]
    public async Task PolicyIsNull_WhenNoPolicyIsApplied()
    {
        ContentSecurityPolicyDefinition? policy = null;
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(),
            app => app.UseContentSecurityPolicy(),
            app => app.Map("/", ctx => Observe(ctx, c => c.ContentSecurityPolicy, out policy)));

        await client.GetAsync("/");
        await Assert.That(policy).IsNull();
    }

    [Test]
    public async Task PolicyIsNotNull_WhenFallbackPolicyIsApplied()
    {
        var expectedPolicy = new ContentSecurityPolicyBuilder().AddDefaultSource(Allow.Self).Build();
        ContentSecurityPolicyDefinition? policy = null;
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.FallbackPolicy = expectedPolicy),
            app => app.UseContentSecurityPolicy(),
            app => app.Map("/", ctx => Observe(ctx, c => c.ContentSecurityPolicy, out policy)));

        await client.GetAsync("/");
        await Assert.That(policy).IsSameReferenceAs(expectedPolicy);
    }

    [Test]
    public async Task PolicyIsNotNull_WhenDefaultPolicyIsApplied()
    {
        var expectedPolicy = new ContentSecurityPolicyBuilder().AddDefaultSource(Allow.Self).Build();
        ContentSecurityPolicyDefinition? policy = null;
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.DefaultPolicy = expectedPolicy),
            app => app.UseContentSecurityPolicy(),
            app => app.Map("/", ctx => Observe(ctx, c => c.ContentSecurityPolicy, out policy)).WithContentSecurityPolicy());

        await client.GetAsync("/");
        await Assert.That(policy).IsSameReferenceAs(expectedPolicy);
    }

    [Test]
    public async Task PolicyIsNotNull_WhenSpecificPolicyIsApplied()
    {
        var expectedPolicy = new ContentSecurityPolicyBuilder().AddDefaultSource(Allow.Self).Build();
        ContentSecurityPolicyDefinition? policy = null;
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.AddPolicy("Specific", expectedPolicy)),
            app => app.UseContentSecurityPolicy(),
            app => app.Map("/", ctx => Observe(ctx, c => c.ContentSecurityPolicy, out policy)).WithContentSecurityPolicy("Specific"));

        await client.GetAsync("/");
        await Assert.That(policy).IsSameReferenceAs(expectedPolicy);
    }

    private static Task Observe<T>(HttpContext httpContext, Func<HttpContext, T?> value, out T? receivedValue) where T : notnull
    {
        receivedValue = value(httpContext);
        return Task.CompletedTask;
    }
}
