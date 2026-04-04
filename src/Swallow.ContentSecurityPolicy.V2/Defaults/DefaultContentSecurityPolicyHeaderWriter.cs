using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Swallow.ContentSecurityPolicy.Abstractions.V2;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.V2.Defaults;

/// <inheritdoc />
public sealed class DefaultContentSecurityPolicyHeaderWriter(ILogger<DefaultContentSecurityPolicyHeaderWriter> logger) : IContentSecurityPolicyHeaderWriter
{
    /// <inheritdoc />
    public void WriteTo(IHeaderDictionary headers, ContentSecurityPolicyDefinition policy, ContentSecurityPolicyWriterContext context)
    {
        AddReportingEndpointsHeader(headers, policy, context.LocalReportingUri);

        var targetHeader = policy.ReportOnly ? HeaderNames.ContentSecurityPolicyReportOnly : HeaderNames.ContentSecurityPolicy;
        var directives = policy.Directives.Select(d => FormatDirective(d, context.Nonce));

        headers.Append(targetHeader, string.Join("; ", directives));
    }

    private void AddReportingEndpointsHeader(IHeaderDictionary headers, ContentSecurityPolicyDefinition policy, string? localReportingUri)
    {
        if (policy.ReportTo is not { } reportTo)
        {
            return;
        }

        if (reportTo is { Url: ReportToDirective.LocalEndpointUri } && localReportingUri is null)
        {
            logger.LogWarning("CSP is set to report to a local endpoint, but the local endpoint could not be resolved.");
            return;
        }

        var url = reportTo.Url is ReportToDirective.LocalEndpointUri ?  localReportingUri : reportTo.Url;
        headers.Append("Reporting-Endpoints", $"{reportTo.EndpointName}=\"{url}\"");
    }

    private static string FormatDirective(Directive directive, string nonce)
    {
        if (directive is IFetchDirective { Expressions: var sourceExpressions })
        {
            var expressions = sourceExpressions.Select(e => FormatExpression(e, nonce));
            return $"{DirectiveName(directive)} {string.Join(' ', expressions)}";
        }

        return directive switch
        {
            ReportToDirective reportTo => $"{DirectiveName(directive)} {reportTo.EndpointName}",
            UpgradeInsecureRequestsDirective => DirectiveName(directive),
            _ => throw new ArgumentOutOfRangeException(nameof(directive))
        };
    }

    private static string DirectiveName(Directive directive)
    {
        return directive switch
        {
            BaseUriDirective => "base-uri",
            ChildSourceDirective => "child-src",
            ConnectSourceDirective => "connect-src",
            DefaultSourceDirective => "default-src",
            FontSourceDirective => "font-src",
            FormActionDirective => "form-action",
            FrameAncestorsDirective => "frame-ancestors",
            FrameSourceDirective => "frame-src",
            ImageSourceDirective => "image-src",
            ManifestSourceDirective => "manifest-src",
            MediaSourceDirective => "media-src",
            ObjectSourceDirective => "object-src",
            ReportToDirective => "report-to",
            ScriptSourceDirective => "script-src",
            ScriptSourceAttributeDirective => "script-src-attr",
            ScriptSourceElementDirective => "script-src-elem",
            StyleSourceDirective => "style-src",
            StyleSourceAttributeDirective => "style-src-attr",
            StyleSourceElementDirective => "style-src-elem",
            UpgradeInsecureRequestsDirective => "upgrade-insecure-requests",
            WorkerSourceDirective => "worker-src",
            _ => throw new ArgumentOutOfRangeException(nameof(directive))
        };
    }

    private static string FormatExpression(ISourceExpression expression, string nonce)
    {
        return expression switch
        {
            DenyAll => "'none'",
            Hash { HashAlgorithm: Hash.Algorithm.SHA256, HashedValue: var hash } => $"'sha256-{hash}'",
            Hash { HashAlgorithm: Hash.Algorithm.SHA384, HashedValue: var hash } => $"'sha384-{hash}'",
            Hash { HashAlgorithm: Hash.Algorithm.SHA512, HashedValue: var hash } => $"'sha512-{hash}'",
            HostSource { Host: var host } => host,
            InlineSpeculationRules => "'inline-speculation-rules'",
            Nonce => $"'nonce-{nonce}'",
            ReportSample => "'report-sample'",
            SchemeSource { Scheme: var scheme } => scheme,
            Self => "'self'",
            StrictDynamic => "'strict-dynamic'",
            TrustedTypesEval => "'trusted-types-eval'",
            UnsafeEval => "'unsafe-eval'",
            UnsafeHashes => "'unsafe-hashes'",
            UnsafeInline => "'unsafe-inline'",
            WasmUnsafeEval => "'wasm-unsafe-eval'",
            _ => throw new ArgumentOutOfRangeException(nameof(expression))
        };
    }
}
