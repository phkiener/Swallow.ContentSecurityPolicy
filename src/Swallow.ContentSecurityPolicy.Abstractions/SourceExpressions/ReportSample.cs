namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

public sealed class ReportSample : FetchSourceExpression
{
    public static readonly ReportSample Instance = new();

    public override string Value => "'report-sample'";
}
