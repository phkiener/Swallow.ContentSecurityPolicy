using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Feature;

namespace Swallow.ContentSecurityPolicy.Tests;

public sealed class ResponseHeadersTest : EndToEndTestBase
{
    [Test]
    public async Task CrashesAtStartup_WhenServicesNotRegistered()
    {
        var excpetion = Assert.Throws<InvalidOperationException>(
            () => GetClient(_ => { }, app => app.UseContentSecurityPolicy()));

        await Assert.That(excpetion.Message).Contains(nameof(ServiceProviderConfig.AddContentSecurityPolicy));
    }

    [Test]
    public async Task DoesNotSendHeaders_WhenNoPolicyConfigured()
    {
        using var client = GetClient(services => services.AddContentSecurityPolicy(), app => app.UseContentSecurityPolicy());
        var response = await client.GetAsync("/");

        await Assert.That(response.Headers.Contains("Content-Security-Policy")).IsFalse();
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.Contains("Reporting-Endpoints")).IsFalse();
    }

    [Test]
    public async Task SendsDefaultPolicy_WhenNoSpecificPolicyDefined()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self))),
            app => app.UseContentSecurityPolicy());

        var response = await client.GetAsync("/");

        await Assert.That(response.Headers.GetValues("Content-Security-Policy")).IsEquivalentTo(["default-src 'self'"]);
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.Contains("Reporting-Endpoints")).IsFalse();
    }

    [Test]
    public async Task SendsDefaultPolicy_WhenEndpointNotFound()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self))),
            app => app.UseContentSecurityPolicy());

        var response = await client.GetAsync("/schnitzel");

        await Assert.That(response.Headers.GetValues("Content-Security-Policy")).IsEquivalentTo(["default-src 'self'"]);
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.Contains("Reporting-Endpoints")).IsFalse();
    }

    [Test]
    public async Task SendsSpecificPolicy_WhenSpecificPolicyDefined()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(
                opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self))
                    .AddPolicy("Specific", b => b.AddDefaultSource(Allow.Nothing))),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.MapGet("/specific", () => "Hello World").WithMetadata(new ContentSecurityPolicyAttribute("Specific"));
            });

        var response = await client.GetAsync("/specific");

        await Assert.That(response.Headers.GetValues("Content-Security-Policy")).IsEquivalentTo(["default-src 'none'"]);
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.Contains("Reporting-Endpoints")).IsFalse();
    }

    [Test]
    public async Task DoesNotSendHeaders_WhenSpecificPolicyDoesNotExist()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(
                opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self))
                    .AddPolicy("Specific", b => b.AddDefaultSource(Allow.Nothing))),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.MapGet("/specific", () => "Hello World").WithMetadata(new ContentSecurityPolicyAttribute("i don't exist"));
            });

        var response = await client.GetAsync("/specific");

        await Assert.That(response.Headers.Contains("Content-Security-Policy")).IsFalse();
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.Contains("Reporting-Endpoints")).IsFalse();
    }

    [Test]
    public async Task DoesNotSendHeaders_WhenDefaultPolicyShouldBeIgnored()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self))),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.MapGet("/ignore", () => "Hello World").WithMetadata(new IgnoreContentSecurityPolicyAttribute());
            });

        var response = await client.GetAsync("/ignore");

        await Assert.That(response.Headers.Contains("Content-Security-Policy")).IsFalse();
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.Contains("Reporting-Endpoints")).IsFalse();
    }

    [Test]
    public async Task SendsReportOnlyHeader_WhenReportingIsSet()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self).ReportOnly())),
            app => app.UseContentSecurityPolicy());

        var response = await client.GetAsync("/");

        await Assert.That(response.Headers.Contains("Content-Security-Policy")).IsFalse();
        await Assert.That(response.Headers.GetValues("Content-Security-Policy-Report-Only")).IsEquivalentTo(["default-src 'self'"]);
        await Assert.That(response.Headers.Contains("Reporting-Endpoints")).IsFalse();
    }

    [Test]
    public async Task SendsReportingHeaders_ForGeneralReporting()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self).SendReportsTo("/endpoint", "report"))),
            app => app.UseContentSecurityPolicy());

        var response = await client.GetAsync("/");

        await Assert.That(response.Headers.GetValues("Content-Security-Policy")).IsEquivalentTo(["default-src 'self'; report-to report"]);
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.GetValues("Reporting-Endpoints")).IsEquivalentTo(["report=\"/endpoint\""]);
    }

    [Test]
    public async Task SkipsReportingEndpoints_WhenLocalEndpointNotConfigured()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self).SendReportsToLocal("report"))),
            app => app.UseContentSecurityPolicy());

        var response = await client.GetAsync("/");

        await Assert.That(response.Headers.GetValues("Content-Security-Policy")).IsEquivalentTo(["default-src 'self'; report-to report"]);
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.Contains("Reporting-Endpoints")).IsFalse();
    }

    [Test]
    public async Task ResolvesLocalReportingEndpoint()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self).SendReportsToLocal("report"))),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.MapContentSecurityPolicyViolations("custom-route");
            });

        var response = await client.GetAsync("/");

        await Assert.That(response.Headers.GetValues("Content-Security-Policy")).IsEquivalentTo(["default-src 'self'; report-to report"]);
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.GetValues("Reporting-Endpoints")).IsEquivalentTo(["report=\"/custom-route\""]);
    }

    [Test]
    public async Task LocalReportingEndpoint_HasCorrectDefaultRoute()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self).SendReportsToLocal("report"))),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.MapContentSecurityPolicyViolations();
            });

        var response = await client.GetAsync("/");

        await Assert.That(response.Headers.GetValues("Content-Security-Policy")).IsEquivalentTo(["default-src 'self'; report-to report"]);
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.GetValues("Reporting-Endpoints")).IsEquivalentTo(["report=\"/_framework/content-security-policy/violations\""]);
    }

    [Test]
    public async Task LocalReportingEndpoint_HasCorrectPathWithPathBase()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self).SendReportsToLocal("report"))),
            app =>
            {
                app.UsePathBase("/tests");
                app.UseContentSecurityPolicy();
                app.MapContentSecurityPolicyViolations("custom-route");
            });

        var response = await client.GetAsync("/tests");

        await Assert.That(response.Headers.GetValues("Content-Security-Policy")).IsEquivalentTo(["default-src 'self'; report-to report"]);
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.GetValues("Reporting-Endpoints")).IsEquivalentTo(["report=\"/tests/custom-route\""]);
    }

    [Test]
    public async Task UsesExpectedNonceInHeader()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Nonce))),
            app =>
            {
                app.UseContentSecurityPolicy();
                app.Map("/nonce", ctx => ctx.Response.WriteAsync(ctx.Nonce ?? ""));
            });

        var response = await client.GetAsync("/nonce");
        var nonce = await response.Content.ReadAsStringAsync();

        await Assert.That(response.Headers.GetValues("Content-Security-Policy")).IsEquivalentTo([$"default-src 'nonce-{nonce}'"]);
        await Assert.That(response.Headers.Contains("Content-Security-Policy-Report-Only")).IsFalse();
        await Assert.That(response.Headers.Contains("Reporting-Endpoints")).IsFalse();
    }
}
