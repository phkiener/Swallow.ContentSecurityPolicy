using Microsoft.Net.Http.Headers;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Reports;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

public sealed class ResponseHeadersTest
{
    [Test]
    public async Task ContentSecurityPolicyNotRegistered_HostDoesNotStart()
    {
        var exception = Assert.Throws<InvalidOperationException>(static () => TestableHost.Start());
        await Assert.That(exception.Message).Contains(nameof(Setup.AddContentSecurityPolicy));
    }

    [Test]
    public async Task NoContentSecurityPolicy_ByDefault()
    {
        await using var host = TestableHost.Start(static s => s.AddContentSecurityPolicy());

        using var client = host.CreateClient();
        using var response = await client.GetAsync("/");

        var hasHeader = response.Headers.TryGetValues(HeaderNames.ContentSecurityPolicy, out _);
        await Assert.That(hasHeader).IsFalse();
    }

    [Test]
    public async Task WithContentSecurityPolicy_ReturnsHeader()
    {
        var policy = new Abstractions.ContentSecurityPolicy
        {
            DefaultSource = [],
            ScriptSource = [Self.Instance],
            StyleSource = [Self.Instance, UnsafeInline.Instance],
        };

        await using var host = TestableHost.Start(s => s.AddContentSecurityPolicy().SetPolicy(policy));

        using var client = host.CreateClient();
        using var response = await client.GetAsync("/");

        var contentSecurityPolicyHeader = response.Headers.GetValues(HeaderNames.ContentSecurityPolicy).FirstOrDefault();
        await Assert.That(contentSecurityPolicyHeader).IsEqualTo("default-src 'none'; script-src 'self'; style-src 'self' 'unsafe-inline'");
    }

    [Test]
    public async Task WithReportOnlyPolicy_UsesCorrectHeader()
    {
        var policy = new Abstractions.ContentSecurityPolicy
        {
            DefaultSource = [],
            ReportOnly = true
        };

        await using var host = TestableHost.Start(s => s.AddContentSecurityPolicy().SetPolicy(policy));

        using var client = host.CreateClient();
        using var response = await client.GetAsync("/");

        var contentSecurityPolicyReportOnlyHeader = response.Headers.GetValues(HeaderNames.ContentSecurityPolicyReportOnly).FirstOrDefault();
        await Assert.That(contentSecurityPolicyReportOnlyHeader).IsEqualTo("default-src 'none'");
    }

    [Test]
    public async Task WithReportingEndpoint_SendsReportTo()
    {
        var policy = new Abstractions.ContentSecurityPolicy { DefaultSource = [] };

        await using var host = TestableHost.Start(
            s => s.AddContentSecurityPolicy()
                .SetPolicy(policy)
                .UseReportHandler<MockReportHandler>());

        using var client = host.CreateClient();
        using var response = await client.GetAsync("/");

        var contentSecurityPolicyHeader = response.Headers.GetValues(HeaderNames.ContentSecurityPolicy).FirstOrDefault();
        await Assert.That(contentSecurityPolicyHeader).IsEqualTo("default-src 'none'; report-to csp-reports");

        var reportingEndpointsHeader = response.Headers.GetValues("Reporting-Endpoints").FirstOrDefault();
        await Assert.That(reportingEndpointsHeader).IsEqualTo("csp-reports=\"/_csp/report\"");
    }

    [Test]
    public async Task WithCustomReportingEndpointName_UsesCustomNameInHeaders()
    {
        var policy = new Abstractions.ContentSecurityPolicy { DefaultSource = [] };

        await using var host = TestableHost.Start(
            s => s.AddContentSecurityPolicy()
                .SetPolicy(policy)
                .UseReportHandler<MockReportHandler>()
                .SetReportingEndpointName("schnitzel"));

        using var client = host.CreateClient();
        using var response = await client.GetAsync("/");

        var contentSecurityPolicyHeader = response.Headers.GetValues(HeaderNames.ContentSecurityPolicy).FirstOrDefault();
        await Assert.That(contentSecurityPolicyHeader).IsEqualTo("default-src 'none'; report-to schnitzel");

        var reportingEndpointsHeader = response.Headers.GetValues("Reporting-Endpoints").FirstOrDefault();
        await Assert.That(reportingEndpointsHeader).IsEqualTo("schnitzel=\"/_csp/report\"");
    }

    [Test]
    public async Task WithCustomReportingEndpointRoute_UsesCustomRouteInHeaders()
    {
        var policy = new Abstractions.ContentSecurityPolicy { DefaultSource = [] };

        await using var host = TestableHost.Start(
            s => s.AddContentSecurityPolicy()
                .SetPolicy(policy)
                .UseReportHandler<MockReportHandler>(),
            c => c.AddCommandLine(["CustomReportRoute=_custom-endpoint"]));

        using var client = host.CreateClient();
        using var response = await client.GetAsync("/");

        var contentSecurityPolicyHeader = response.Headers.GetValues(HeaderNames.ContentSecurityPolicy).FirstOrDefault();
        await Assert.That(contentSecurityPolicyHeader).IsEqualTo("default-src 'none'; report-to csp-reports");

        var reportingEndpointsHeader = response.Headers.GetValues("Reporting-Endpoints").FirstOrDefault();
        await Assert.That(reportingEndpointsHeader).IsEqualTo("csp-reports=\"/_custom-endpoint\"");
    }

    [Test]
    public async Task WithNonce_NonceInHeaderMatches()
    {
        var policy = new Abstractions.ContentSecurityPolicy { DefaultSource = [Nonce.Instance] };

        await using var host = TestableHost.Start(s => s.AddContentSecurityPolicy().SetPolicy(policy));

        using var client = host.CreateClient();
        using var response = await client.GetAsync("/nonce");

        var nonce = await response.Content.ReadAsStringAsync();
        var contentSecurityPolicyHeader = response.Headers.GetValues(HeaderNames.ContentSecurityPolicy).FirstOrDefault();
        await Assert.That(contentSecurityPolicyHeader).IsEqualTo($"default-src 'nonce-{nonce}'");
    }

    [Test]
    public async Task WithCustomNonceGenerator_UsesGeneratedNonce()
    {
        var policy = new Abstractions.ContentSecurityPolicy { DefaultSource = [Nonce.Instance] };

        await using var host = TestableHost.Start(
            s => s.AddSingleton<INonceGenerator, MockNonceGenerator>()
                .AddContentSecurityPolicy()
                .SetPolicy(policy));

        using var client = host.CreateClient();
        using var response = await client.GetAsync("/");

        var contentSecurityPolicyHeader = response.Headers.GetValues(HeaderNames.ContentSecurityPolicy).FirstOrDefault();
        await Assert.That(contentSecurityPolicyHeader).IsEqualTo($"default-src 'nonce-{Assertion.TestNonce}'");
    }

    [Test]
    public async Task WithEndpointModifyingPolicy_UsesUpdatedPolicyInHeaders()
    {
        var policy = new Abstractions.ContentSecurityPolicy { DefaultSource = [DenyAll.Instance] };

        await using var host = TestableHost.Start(s => s.AddContentSecurityPolicy().SetPolicy(policy));

        using var client = host.CreateClient();
        using var response = await client.GetAsync("/unsafe-inline");

        var contentSecurityPolicyHeader = response.Headers.GetValues(HeaderNames.ContentSecurityPolicy).FirstOrDefault();
        await Assert.That(contentSecurityPolicyHeader).IsEqualTo($"default-src 'unsafe-inline'");
    }

    private sealed class MockReportHandler : IReportHandler
    {
        public Task Handle(CSPViolationReport[] violationReports, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    private sealed class MockNonceGenerator : INonceGenerator
    {
        public string Generate() => Assertion.TestNonce;
    }
}
