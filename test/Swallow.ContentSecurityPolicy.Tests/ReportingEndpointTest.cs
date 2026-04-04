using System.Net;
using System.Net.Http.Headers;
using Swallow.ContentSecurityPolicy.Reports;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

public sealed class ReportingEndpointTest
{
    private static readonly Abstractions.ContentSecurityPolicy DenyAll = new() { DefaultSource = [] };

    [Test]
    public async Task ReturnsNotFound_WhenNoHandlersAreRegistered()
    {
        await using var host = TestableHost.Start(s => s.AddContentSecurityPolicy().SetPolicy(DenyAll));

        var report = new CSPViolationReport
        {
            Body = new CSPViolationReport.ReportBody
            {
                BlockedUrl = "https://localhost:81/hacked.js",
                ColumnNumber = null,
                Disposition = CSPViolationReport.Disposition.Enforce,
                DocumentUrl = "https://localhost:80/",
                EffectiveDirective = "default-src",
                LineNumber = null,
                OriginalPolicy = "default-src 'none'",
                Referrer = null,
                Sample = "",
                SourceFile = null,
                StatusCode = 200
            },
            Url = "https://localhost:80/"
        };

        using var client = host.CreateClient();
        var response = await client.PostAsync("/_csp/report", JsonContent.Create(report, mediaType: new MediaTypeHeaderValue("application/reports+json")));

        await Assert.That(response).HasStatusCode(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task ReportingEndpoint_HandlesSingleViolation()
    {
        var reportHandler = new TestableReportHandler();
        await using var host = TestableHost.Start(
            s => s.AddSingleton<IReportHandler>(reportHandler)
                .AddContentSecurityPolicy()
                .SetPolicy(DenyAll)
                .UseReportHandler<TestableReportHandler>());

        var report = new CSPViolationReport
        {
            Body = new CSPViolationReport.ReportBody
            {
                BlockedUrl = "https://localhost:81/hacked.js",
                ColumnNumber = null,
                Disposition = CSPViolationReport.Disposition.Enforce,
                DocumentUrl = "https://localhost:80/",
                EffectiveDirective = "default-src",
                LineNumber = null,
                OriginalPolicy = "default-src 'none'",
                Referrer = null,
                Sample = "",
                SourceFile = null,
                StatusCode = 200
            },
            Url = "https://localhost:80/"
        };

        using var client = host.CreateClient();
        await client.PostAsync("/_csp/report", JsonContent.Create(report, mediaType: new MediaTypeHeaderValue("application/reports+json")));

        await Assert.That(reportHandler.Reports).Count().IsEqualTo(1);
    }

    [Test]
    public async Task ReportingEndpointWithCustomRoute_GetsInvokedOnCustomRoute()
    {
        var reportHandler = new TestableReportHandler();
        await using var host = TestableHost.Start(
            s => s.AddSingleton<IReportHandler>(reportHandler)
                .AddContentSecurityPolicy()
                .SetPolicy(DenyAll)
                .UseReportHandler<TestableReportHandler>(),
        c => c.AddCommandLine(["CustomReportRoute=_custom-endpoint"]));

        var report = new CSPViolationReport
        {
            Body = new CSPViolationReport.ReportBody
            {
                BlockedUrl = "https://localhost:81/hacked.js",
                ColumnNumber = null,
                Disposition = CSPViolationReport.Disposition.Enforce,
                DocumentUrl = "https://localhost:80/",
                EffectiveDirective = "default-src",
                LineNumber = null,
                OriginalPolicy = "default-src 'none'",
                Referrer = null,
                Sample = "",
                SourceFile = null,
                StatusCode = 200
            },
            Url = "https://localhost:80/"
        };

        using var client = host.CreateClient();
        await client.PostAsync("/_custom-endpoint", JsonContent.Create(report, mediaType: new MediaTypeHeaderValue("application/reports+json")));

        await Assert.That(reportHandler.Reports).Count().IsEqualTo(1);
    }

    [Test]
    public async Task ReportingEndpoint_HandlesArrayOfViolations()
    {
        var reportHandler = new TestableReportHandler();
        await using var host = TestableHost.Start(
            s => s.AddSingleton<IReportHandler>(reportHandler)
                .AddContentSecurityPolicy()
                .SetPolicy(DenyAll)
                .UseReportHandler<TestableReportHandler>());

        var report = new CSPViolationReport
        {
            Body = new CSPViolationReport.ReportBody
            {
                BlockedUrl = "https://localhost:81/hacked.js",
                ColumnNumber = null,
                Disposition = CSPViolationReport.Disposition.Enforce,
                DocumentUrl = "https://localhost:80/",
                EffectiveDirective = "default-src",
                LineNumber = null,
                OriginalPolicy = "default-src 'none'",
                Referrer = null,
                Sample = "",
                SourceFile = null,
                StatusCode = 200
            },
            Url = "https://localhost:80/"
        };

        using var client = host.CreateClient();
        await client.PostAsync("/_csp/report", JsonContent.Create(new List<CSPViolationReport> { report, report, report }, mediaType: new MediaTypeHeaderValue("application/reports+json")));

        await Assert.That(reportHandler.Reports).Count().IsEqualTo(3);
    }

    private sealed class TestableReportHandler : IReportHandler
    {
        private readonly List<CSPViolationReport> receivedReports = [];

        public Task Handle(CSPViolationReport[] violationReports, CancellationToken cancellationToken)
        {
            receivedReports.AddRange(violationReports);
            return Task.CompletedTask;
        }

        public IReadOnlyList<CSPViolationReport> Reports => receivedReports.AsReadOnly();
    }
}
