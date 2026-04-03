namespace Swallow.ContentSecurityPolicy.Reports;

/// <summary>
/// A handler to process CPS violation reports.
/// </summary>
public interface IReportHandler
{
    /// <summary>
    /// Handle the given violation report.
    /// </summary>
    /// <param name="violationReports">The violation reports that were generated.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to abort any asynchronous operation.</param>
    Task Handle(CSPViolationReport[] violationReports, CancellationToken cancellationToken);
}
