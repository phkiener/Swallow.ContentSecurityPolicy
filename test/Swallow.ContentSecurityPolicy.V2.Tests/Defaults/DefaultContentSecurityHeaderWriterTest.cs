using Microsoft.Extensions.Logging.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.V2;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;
using Swallow.ContentSecurityPolicy.V2.Defaults;

namespace Swallow.ContentSecurityPolicy.V2.Tests.Defaults;

public sealed class DefaultContentSecurityPolicyHeaderWriterTest
{
    private readonly ContentSecurityPolicyWriterContext context = new("NONCE", "/local");
    private DefaultContentSecurityPolicyHeaderWriter HeaderWriter => new(NullLogger<DefaultContentSecurityPolicyHeaderWriter>.Instance);

    [Test]
    public async Task WritesNothing_WhenPolicyHasNoDirectives()
    {
        var policy = new ContentSecurityPolicyBuilder().Build();
        var headers = new HeaderDictionary();
        HeaderWriter.WriteTo(headers, policy, context);

        await Assert.That(headers).DoesNotContainKey("Content-Security-Policy");
        await Assert.That(headers).DoesNotContainKey("Content-Security-Policy-Report-Only");
        await Assert.That(headers).DoesNotContainKey("Reporting-Endpoints");
    }

    [Test]
    public async Task WritesEmptyFetchDirectives()
    {
        await AssertContentSecurityPolicy(b => b.AddBaseUri(), "base-uri 'none'");
        await AssertContentSecurityPolicy(b => b.AddChildSource(), "child-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddConnectSource(), "connect-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(), "default-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddFontSource(), "font-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddFormAction(), "form-action 'none'");
        await AssertContentSecurityPolicy(b => b.AddFrameAncestors(), "frame-ancestors 'none'");
        await AssertContentSecurityPolicy(b => b.AddFrameSource(), "frame-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddImageSource(), "image-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddManifestSource(), "manifest-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddMediaSource(), "media-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddScriptSource(), "script-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddScriptSourceAttribute(), "script-src-attr 'none'");
        await AssertContentSecurityPolicy(b => b.AddScriptSourceElement(), "script-src-elem 'none'");
        await AssertContentSecurityPolicy(b => b.AddStyleSource(), "style-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddStyleSourceAttribute(), "style-src-attr 'none'");
        await AssertContentSecurityPolicy(b => b.AddStyleSourceElement(), "style-src-elem 'none'");
        await AssertContentSecurityPolicy(b => b.AddWorkerSource(), "worker-src 'none'");
    }

    [Test]
    public async Task WritesSourceExpressions()
    {
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Nothing), "default-src 'none'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Hash(Hash.Algorithm.SHA256, "AA==")), "default-src 'sha256-AA=='");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Hash(Hash.Algorithm.SHA256, [0x00])), "default-src 'sha256-AA=='");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Hash(Hash.Algorithm.SHA384, "AA==")), "default-src 'sha384-AA=='");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Hash(Hash.Algorithm.SHA384, [0x00])), "default-src 'sha384-AA=='");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Hash(Hash.Algorithm.SHA512, "AA==")), "default-src 'sha512-AA=='");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Hash(Hash.Algorithm.SHA512, [0x00])), "default-src 'sha512-AA=='");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Host("https://localhost:80/")), "default-src https://localhost:80/");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.InlineSpeculationRules), "default-src 'inline-speculation-rules'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Nonce), $"default-src 'nonce-{context.Nonce}'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.ReportSample), "default-src 'report-sample'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Scheme("http")), "default-src http:");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Scheme("ws:")), "default-src ws:");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.Self), "default-src 'self'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.StrictDynamic), "default-src 'strict-dynamic'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.TrustedTypesEval), "default-src 'trusted-types-eval'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.UnsafeEval), "default-src 'unsafe-eval'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.UnsafeHashes), "default-src 'unsafe-hashes'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.UnsafeInline), "default-src 'unsafe-inline'");
        await AssertContentSecurityPolicy(b => b.AddDefaultSource(Allow.WasmUnsafeEval), "default-src 'wasm-unsafe-eval'");
    }

    [Test]
    public async Task WritesMultipleDirectivesWithMultipleExpressions()
    {
        await AssertContentSecurityPolicy(
            b => b
                .SetUpgradeInsecureRequests()
                .AddScriptSource(Allow.Nonce, Allow.Self)
                .AddStyleSource(Allow.UnsafeInline, Allow.Self),
            $"upgrade-insecure-requests; script-src 'nonce-{context.Nonce}' 'self'; style-src 'unsafe-inline' 'self'");
    }

    [Test]
    public async Task WritesReportOnlyToCorrectHeader()
    {
        await AssertContentSecurityPolicy(
            builder: b => b.AddDefaultSource(Allow.Nothing).ReportOnly(),
            expectedValue: "default-src 'none'",
            header: "Content-Security-Policy-Report-Only");
    }

    [Test]
    public async Task WritesReportingEndpoints()
    {
        var policy = new ContentSecurityPolicyBuilder().SendReportsTo("/csp", "my-endpoint").Build();
        var headers = new HeaderDictionary();
        HeaderWriter.WriteTo(headers, policy, context);

        await Assert.That(headers["Content-Security-Policy"].ToString()).IsEqualTo("report-to my-endpoint");
        await Assert.That(headers["Reporting-Endpoints"].ToString()).IsEqualTo("my-endpoint=\"/csp\"");
    }

    [Test]
    public async Task WritesLocalReportingEndpoints()
    {
        var policy = new ContentSecurityPolicyBuilder().SendReportsToLocal("my-endpoint").Build();
        var headers = new HeaderDictionary();
        HeaderWriter.WriteTo(headers, policy, context);

        await Assert.That(headers["Content-Security-Policy"].ToString()).IsEqualTo("report-to my-endpoint");
        await Assert.That(headers["Reporting-Endpoints"].ToString()).IsEqualTo($"my-endpoint=\"{context.LocalReportingUri}\"");
    }

    [Test]
    public async Task SkipsReportingEndpoints_WhenLocalEndpointIsNotSet()
    {
        var policy = new ContentSecurityPolicyBuilder().SendReportsToLocal("my-endpoint").Build();
        var headers = new HeaderDictionary();
        HeaderWriter.WriteTo(headers, policy, new ContentSecurityPolicyWriterContext("NONCE", null));

        await Assert.That(headers["Content-Security-Policy"].ToString()).IsEqualTo("report-to my-endpoint");
        await Assert.That(headers).DoesNotContainKey("Reporting-Endpoints");
    }

    private async Task AssertContentSecurityPolicy(Action<ContentSecurityPolicyBuilder> builder, string expectedValue, string header = "Content-Security-Policy")
    {
        var policyBuilder = new ContentSecurityPolicyBuilder();
        builder(policyBuilder);

        var headers = new HeaderDictionary();
        HeaderWriter.WriteTo(headers, policyBuilder.Build(), context);

        await Assert.That(headers[header].ToString()).IsEqualTo(expectedValue);
    }
}
