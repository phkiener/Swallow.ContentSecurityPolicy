using System.Text.Json;
using Swallow.ContentSecurityPolicy.Abstractions.Reports;

namespace DemoHost;

public sealed class ReportHandler(ILogger<ReportHandler> logger) : IReportHandler
{
    public Task Handle(ViolationReport[] violationReports, CancellationToken cancellationToken)
    {
        foreach (var report in violationReports)
        {
            logger.LogWarning("CSP Violation {Report}", JsonSerializer.Serialize(report));
        }

        return Task.CompletedTask;
    }
}
