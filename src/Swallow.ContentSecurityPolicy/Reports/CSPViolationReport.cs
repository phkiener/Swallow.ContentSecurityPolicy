using System.Text.Json.Serialization;

namespace Swallow.ContentSecurityPolicy.Reports;

/// <summary>
/// A CSP violation report sent by the browser.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport">CSPViolationReport on MDN</seealso>
public sealed class CSPViolationReport
{
    /// <summary>
    /// The body of the report.
    /// </summary>
    [JsonPropertyName("body"), JsonRequired]
    public required ReportBody Body { get; init; }

    /// <summary>
    /// The URL that generated the report.
    /// </summary>
    [JsonPropertyName("url"), JsonRequired]
    public required string Url { get; init; }

    /// <summary>
    /// The body of a CSP violation report.
    /// </summary>
    public sealed class ReportBody
    {
        /// <summary>
        /// The resource that was blocked due to the CSP.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/blockedURL">BlockedUrl on MDN</seealso>
        [JsonPropertyName("blockedURL"), JsonRequired]
        public required string BlockedUrl { get; init; }

        /// <summary>
        /// Column number in the source file where the violation happened.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/columnNumber">ColumnNumber on MDN</seealso>
        [JsonPropertyName("columnNumber"), JsonRequired]
        public required int? ColumnNumber { get; init; }

        /// <summary>
        /// Whether the CSP was enforced or only sent a report.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/disposition">Disposition on MDN</seealso>
        [JsonPropertyName("disposition"), JsonRequired]
        public required Disposition Disposition { get; init; }

        /// <summary>
        /// The URL of the document that violated the CSP.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/documentURL">DocumentUrl on MDN</seealso>
        [JsonPropertyName("documentURL"), JsonRequired]
        public required string DocumentUrl { get; init; }

        /// <summary>
        /// The directive of the CSP that was violated.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/effectiveDirective">EffectiveDirective on MDN</seealso>
        [JsonPropertyName("effectiveDirective"), JsonRequired]
        public required string EffectiveDirective { get; init; }

        /// <summary>
        /// Line number in the source file where the violation happened.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/lineNumber">LineNumber on MDN</seealso>
        [JsonPropertyName("lineNumber"), JsonRequired]
        public required int? LineNumber { get; init; }

        /// <summary>
        /// The policy that was violated.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/originalPolicy">OriginalPolicy on MDN</seealso>
        [JsonPropertyName("originalPolicy"), JsonRequired]
        public required string OriginalPolicy { get; init; }

        /// <summary>
        /// The referrer of the URL that violated the CSP.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/referrer">Referrer on MDN</seealso>
        [JsonPropertyName("referrer"), JsonRequired]
        public required string? Referrer { get; init; }

        /// <summary>
        /// A short sample-content of the resource that violated the CSP.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/sample">Sample on MDN</seealso>
        [JsonPropertyName("sample"), JsonRequired]
        public required string Sample { get; init; }

        /// <summary>
        /// The source file that triggered the violation.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/sourceFile">SourceFile on MDN</seealso>
        [JsonPropertyName("sourceFile"), JsonRequired]
        public required string? SourceFile { get; init; }

        /// <summary>
        /// The status code of the response that triggered the violation.
        /// </summary>
        /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/statusCode">StatusCode on MDN</seealso>
        [JsonPropertyName("statusCode"), JsonRequired]
        public required int StatusCode { get; init; }
    }

    /// <summary>
    /// Whether the CSP was enforced or only sent a report.
    /// </summary>
    /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/CSPViolationReport/disposition">Disposition on MDN</seealso>
    public enum Disposition
    {
        /// <summary>
        /// The CSP was enforced and a resource was blocked.
        /// </summary>
        Enforce,

        /// <summary>
        /// The CSP only reported the violation, the resource was <em>not</em> blocked.
        /// </summary>
        Report
    }
}

[JsonSourceGenerationOptions(Converters = [typeof(JsonStringEnumConverter<CSPViolationReport.Disposition>)])]
[JsonSerializable(typeof(CSPViolationReport))]
internal partial class CspViolationReportSourceGenerationContext : JsonSerializerContext;
