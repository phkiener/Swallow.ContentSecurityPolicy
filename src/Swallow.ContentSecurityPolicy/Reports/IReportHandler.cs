namespace Swallow.ContentSecurityPolicy.Reports;

/// <summary>
/// A handler to process CPS violation reports.
/// </summary>
public interface IReportHandler
{
    /// <summary>
    /// Handle the given violation report.
    /// </summary>
    /// <param name="violationReport">The violation report that was generated.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to abort any asynchronous operation.</param>
    Task Handle(CSPViolationReport violationReport, CancellationToken cancellationToken);
}
