using Swallow.ContentSecurityPolicy.Reports;

namespace DemoHost;

public sealed class ReportHandler(ILogger<ReportHandler> logger) : IReportHandler
{
    public Task Handle(CSPViolationReport violationReport, CancellationToken cancellationToken)
    {
        logger.LogWarning("CSP Violation {Report}", violationReport);

        return Task.CompletedTask;
    }
}
