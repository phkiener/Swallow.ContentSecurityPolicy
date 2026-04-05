using System.Net;
using System.Net.Http.Headers;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Reports;

namespace Swallow.ContentSecurityPolicy.V2.Tests;

public sealed class ViolationReportsTest : EndToEndTestBase
{
    private readonly ViolationReport report = new()
    {
        Body = new ViolationReport.ReportBody
        {
            BlockedUrl = "https://localhost:81/hacked.js",
            ColumnNumber = null,
            Disposition = ViolationReport.Disposition.Enforce,
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

    [Test]
    public async Task ReturnsOK_WhenMapped()
    {
        using var client = GetClient(services => services.AddContentSecurityPolicy(), app => app.MapContentSecurityPolicyViolations("report"));

        var response = await client.PostAsync("/report", JsonContent.Create(report, new MediaTypeHeaderValue("application/reports+json")));
        await Assert.That(response).HasStatusCode(HttpStatusCode.OK);
    }

    [Test]
    public async Task ReturnsUnsupportedMediaType_WhenContentTypeIsWrong()
    {
        using var client = GetClient(services => services.AddContentSecurityPolicy(), app => app.MapContentSecurityPolicyViolations("report"));

        var response = await client.PostAsync("/report", JsonContent.Create(report, new MediaTypeHeaderValue("application/json")));
        await Assert.That(response).HasStatusCode(HttpStatusCode.UnsupportedMediaType);
    }

    [Test]
    public async Task ReturnsBadRequest_OnBrokenJson()
    {
        using var client = GetClient(services => services.AddContentSecurityPolicy(), app => app.MapContentSecurityPolicyViolations("report"));

        var response = await client.PostAsync("/report", JsonContent.Create(new { body = new { report = "unsupported" } }, new MediaTypeHeaderValue("application/reports+json")));
        await Assert.That(response).HasStatusCode(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task InvokesAllReportHandlers()
    {
        var firstReportHandler = new ReportHandler();
        var secondReportHandler = new ReportHandler();

        using var client = GetClient(
            services => services.AddContentSecurityPolicy()
                .AddSingleton<IReportHandler>(firstReportHandler)
                .AddSingleton<IReportHandler>(secondReportHandler),
            app => app.MapContentSecurityPolicyViolations("report"));

        await client.PostAsync("/report", JsonContent.Create(report, new MediaTypeHeaderValue("application/reports+json")));

        await Assert.That(firstReportHandler.ReportsReceived).IsEqualTo(1);
        await Assert.That(secondReportHandler.ReportsReceived).IsEqualTo(1);
    }

    [Test]
    public async Task InvokesAllReportHandlers_EvenIfOneCrashes()
    {
        var firstReportHandler = new ReportHandler(doCrash: true);
        var secondReportHandler = new ReportHandler();

        using var client = GetClient(
            services => services.AddContentSecurityPolicy()
                .AddSingleton<IReportHandler>(firstReportHandler)
                .AddSingleton<IReportHandler>(secondReportHandler),
            app => app.MapContentSecurityPolicyViolations("report"));

        var response = await client.PostAsync("/report", JsonContent.Create(report, new MediaTypeHeaderValue("application/reports+json")));
        await Assert.That(response).HasStatusCode(HttpStatusCode.InternalServerError);

        await Assert.That(firstReportHandler.ReportsReceived).IsEqualTo(1);
        await Assert.That(secondReportHandler.ReportsReceived).IsEqualTo(1);
    }

    [Test]
    public async Task AcceptsListOfReports()
    {
        var reportHandler = new ReportHandler();

        using var client = GetClient(
            services => services.AddContentSecurityPolicy().AddSingleton<IReportHandler>(reportHandler),
            app => app.MapContentSecurityPolicyViolations("report"));

        await client.PostAsync("/report", JsonContent.Create(new List<ViolationReport> { report, report, report }, new MediaTypeHeaderValue("application/reports+json")));
        await Assert.That(reportHandler.ReportsReceived).IsEqualTo(3);
    }

    private sealed class ReportHandler(bool doCrash = false) : IReportHandler
    {
        public int ReportsReceived { get; private set; } = 0;

        public Task Handle(ViolationReport[] violationReports, CancellationToken cancellationToken)
        {
            ReportsReceived += violationReports.Length;
            return doCrash ? throw new InvalidOperationException("Something died") : Task.CompletedTask;
        }
    }
}
