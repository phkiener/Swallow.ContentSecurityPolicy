using System.Net;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Endpoints;
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
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", () => "Hello World"));

        var response = await client.GetAsync("/");
        await AssertHeaders(response);
    }

    [Test]
    public async Task SendsFallbackPolicy_WhenDefined()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetFallbackPolicy(b => b.AddDefaultSource(Allow.Self))),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", () => "Hello World"));

        var response = await client.GetAsync("/");
        await AssertHeaders(response, contentSecurityPolicy: "default-src 'self'");
    }

    [Test]
    public async Task SendsFallbackPolicy_IfEndpointNotFound()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetFallbackPolicy(b => b.AddDefaultSource(Allow.Self))),
            app => app.UseContentSecurityPolicy());

        var response = await client.GetAsync("/foobar");
        await AssertHeaders(response, contentSecurityPolicy: "default-src 'self'");
    }

    [Test]
    public async Task SendsDefaultPolicy_WhenDefinedOnEndpoint()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt
                .SetFallbackPolicy(b => b.AddDefaultSource(Allow.Self))
                .SetDefaultPolicy(b => b.AddDefaultSource(Allow.Nothing))),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", () => "Hello World").WithContentSecurityPolicy());

        var response = await client.GetAsync("/");
        await AssertHeaders(response, contentSecurityPolicy: "default-src 'none'");
    }

    [Test]
    public async Task SendsSpecificPolicy_WhenSpecificPolicyDefined()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt
                .SetFallbackPolicy(b => b.AddDefaultSource(Allow.Self))
                .SetDefaultPolicy(b => b.AddDefaultSource(Allow.Nothing))
                .AddPolicy("specific", b => b.AddDefaultSource(Allow.UnsafeInline))),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", () => "Hello World").WithContentSecurityPolicy("specific"));

        var response = await client.GetAsync("/");
        await AssertHeaders(response, contentSecurityPolicy: "default-src 'unsafe-inline'");
    }

    [Test]
    public async Task ThrowsException_WhenSpecificPolicyIsRequestedButNotDefined()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", () => "Hello World").WithContentSecurityPolicy("specific"));

        var response = await client.GetAsync("/");
        await Assert.That(response).HasStatusCode(HttpStatusCode.InternalServerError);
    }

    [Test]
    public async Task DoesNotSendHeaders_WhenPolicyShouldBeIgnored()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self))),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", () => "Hello World").DisableContentSecurityPolicy());

        var response = await client.GetAsync("/");
        await AssertHeaders(response);
    }

    [Test]
    public async Task SendsReportOnlyHeader_WhenReportingIsSet()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt
                .SetFallbackPolicy(b => b
                    .AddDefaultSource(Allow.Self)
                    .ReportOnly())),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", () => "Hello World"));

        var response = await client.GetAsync("/");
        await AssertHeaders(response, contentSecurityPolicyReportOnly: "default-src 'self'");
    }

    [Test]
    public async Task SendsReportingHeaders()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt
                .SetFallbackPolicy(b => b
                    .AddDefaultSource(Allow.Self)
                    .SendReportsTo("/endpoint", "report"))),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", () => "Hello World"));

        var response = await client.GetAsync("/");
        await AssertHeaders(
            response,
            contentSecurityPolicy: "default-src 'self'; report-to report",
            reportingEndpoints: "report=\"/endpoint\"");
    }

    [Test]
    public async Task SkipsReportingEndpoints_WhenLocalEndpointNotConfigured()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt
                .SetFallbackPolicy(b => b
                    .AddDefaultSource(Allow.Self)
                    .SendReportsToLocal("report"))),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", () => "Hello World"));

        var response = await client.GetAsync("/");
        await AssertHeaders(response, contentSecurityPolicy: "default-src 'self'; report-to report");
    }

    [Test]
    public async Task ResolvesLocalReportingEndpoint()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt
                .SetFallbackPolicy(b => b
                    .AddDefaultSource(Allow.Self)
                    .SendReportsToLocal("report"))),
            app => app.UseContentSecurityPolicy(),
            app => app.MapContentSecurityPolicyViolations("custom-route"),
            app => app.MapGet("/", () => "Hello World"));

        var response = await client.GetAsync("/");
        await AssertHeaders(
            response,
            contentSecurityPolicy: "default-src 'self'; report-to report",
            reportingEndpoints: "report=\"/custom-route\"");
    }

    [Test]
    public async Task LocalReportingEndpoint_HasCorrectDefaultRoute()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt
                .SetFallbackPolicy(b => b
                    .AddDefaultSource(Allow.Self)
                    .SendReportsToLocal("report"))),
            app => app.UseContentSecurityPolicy(),
            app => app.MapContentSecurityPolicyViolations(),
            app => app.MapGet("/", () => "Hello World"));

        var response = await client.GetAsync("/");
        await AssertHeaders(
            response,
            contentSecurityPolicy: "default-src 'self'; report-to report",
            reportingEndpoints: "report=\"/_framework/content-security-policy/violations\"");
    }

    [Test]
    public async Task LocalReportingEndpoint_HasCorrectRouteWithPathBase()
    {
        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt
                .SetFallbackPolicy(b => b
                    .AddDefaultSource(Allow.Self)
                    .SendReportsToLocal("report"))),
            app => app.UsePathBase("/base"),
            app => app.UseContentSecurityPolicy(),
            app => app.MapContentSecurityPolicyViolations(),
            app => app.MapGet("/", () => "Hello World"));

        var response = await client.GetAsync("/base");
        await AssertHeaders(
            response,
            contentSecurityPolicy: "default-src 'self'; report-to report",
            reportingEndpoints: "report=\"/base/_framework/content-security-policy/violations\"");
    }

    [Test]
    public async Task UsesExpectedNonceInHeader()
    {

        using var client = GetClient(
            services => services.AddContentSecurityPolicy(opt => opt.SetFallbackPolicy(b => b.AddDefaultSource(Allow.Nonce))),
            app => app.UseContentSecurityPolicy(),
            app => app.MapGet("/", ctx => ctx.Response.WriteAsync(ctx.Nonce ?? "")));

        var response = await client.GetAsync("/");
        var nonce = await response.Content.ReadAsStringAsync();
        await AssertHeaders(response, contentSecurityPolicy: $"default-src 'nonce-{nonce}'");
    }

    private static async Task AssertHeaders(
        HttpResponseMessage response,
        string? contentSecurityPolicy = null,
        string? contentSecurityPolicyReportOnly = null,
        string? reportingEndpoints = null)
    {
        await AssertHeader(response, "Content-Security-Policy", contentSecurityPolicy);
        await AssertHeader(response, "Content-Security-Policy-Report-Only", contentSecurityPolicyReportOnly);
        await AssertHeader(response, "Reporting-Endpoints", reportingEndpoints);
    }

    private static async Task AssertHeader(HttpResponseMessage response, string header, string? expectedValue)
    {
        if (expectedValue is not null)
        {
            await Assert.That(response.Headers.GetValues(header)).IsEquivalentTo([expectedValue]);
        }
        else
        {
            await Assert.That(response.Headers.Contains(header)).IsFalse();
        }
    }
}
