namespace Swallow.ContentSecurityPolicy.Abstractions.V2.Reports;

/// <summary>
/// A handler to process content security policy violation reports.
/// </summary>
public interface IReportHandler
{
    /// <summary>
    /// Handle the given violation report.
    /// </summary>
    /// <param name="violationReports">The violation reports that were generated.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to abort any asynchronous operation.</param>
    Task Handle(ViolationReport[] violationReports, CancellationToken cancellationToken);
}
