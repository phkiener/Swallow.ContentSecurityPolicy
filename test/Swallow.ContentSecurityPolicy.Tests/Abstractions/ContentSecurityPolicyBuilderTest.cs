using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Model;
using Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Tests.Abstractions;

public sealed class ContentSecurityPolicyBuilderTest
{
    private readonly ContentSecurityPolicyBuilder builder = new();

    [Test]
    public async Task CanBuildEmptyPolicy()
    {
        var policy = builder.Build();
        await Assert.That(policy.Directives).IsEmpty();
    }

    [Test]
    public async Task CanUseConvenienceMethods_ForFetchDirectives()
    {
        await AssertBuilder(b => b.BaseUri, p => p.BaseUri, (b, e) => b.AddBaseUri(e), Allow.Nothing, (b, d) => b.BaseUri = d, Allow.Self, b => b.BaseUri = null);
        await AssertBuilder(b => b.ChildSource, p => p.ChildSource, (b, e) => b.AddChildSource(e), Allow.Nothing, (b, d) => b.ChildSource = d, Allow.Self, b => b.ChildSource = null);
        await AssertBuilder(b => b.ConnectSource, p => p.ConnectSource, (b, e) => b.AddConnectSource(e), Allow.Nothing, (b, d) => b.ConnectSource = d, Allow.Self, b => b.ConnectSource = null);
        await AssertBuilder(b => b.DefaultSource, p => p.DefaultSource, (b, e) => b.AddDefaultSource(e), Allow.Nothing, (b, d) => b.DefaultSource = d, Allow.Self, b => b.DefaultSource = null);
        await AssertBuilder(b => b.FontSource, p => p.FontSource, (b, e) => b.AddFontSource(e), Allow.Nothing, (b, d) => b.FontSource = d, Allow.Self, b => b.FontSource = null);
        await AssertBuilder(b => b.FormAction, p => p.FormAction, (b, e) => b.AddFormAction(e), Allow.Nothing, (b, d) => b.FormAction = d, Allow.Self, b => b.FormAction = null);
        await AssertBuilder(b => b.FrameAncestors, p => p.FrameAncestors, (b, e) => b.AddFrameAncestors(e), Allow.Nothing, (b, d) => b.FrameAncestors = d, Allow.Self, b => b.FrameAncestors = null);
        await AssertBuilder(b => b.FrameSource, p => p.FrameSource, (b, e) => b.AddFrameSource(e), Allow.Nothing, (b, d) => b.FrameSource = d, Allow.Self, b => b.FrameSource = null);
        await AssertBuilder(b => b.ImageSource, p => p.ImageSource, (b, e) => b.AddImageSource(e), Allow.Nothing, (b, d) => b.ImageSource = d, Allow.Self, b => b.ImageSource = null);
        await AssertBuilder(b => b.ManifestSource, p => p.ManifestSource, (b, e) => b.AddManifestSource(e), Allow.Nothing, (b, d) => b.ManifestSource = d, Allow.Self, b => b.ManifestSource = null);
        await AssertBuilder(b => b.MediaSource, p => p.MediaSource, (b, e) => b.AddMediaSource(e), Allow.Nothing, (b, d) => b.MediaSource = d, Allow.Self, b => b.MediaSource = null);
        await AssertBuilder(b => b.ObjectSource, p => p.ObjectSource, (b, e) => b.AddObjectSource(e), Allow.Nothing, (b, d) => b.ObjectSource = d, Allow.Self, b => b.ObjectSource = null);
        await AssertBuilder(b => b.ScriptSource, p => p.ScriptSource, (b, e) => b.AddScriptSource(e), Allow.Nothing, (b, d) => b.ScriptSource = d, Allow.Self, b => b.ScriptSource = null);
        await AssertBuilder(b => b.ScriptSourceAttribute, p => p.ScriptSourceAttribute, (b, e) => b.AddScriptSourceAttribute(e), Allow.Nothing, (b, d) => b.ScriptSourceAttribute = d, Allow.UnsafeInline, b => b.ScriptSourceAttribute = null);
        await AssertBuilder(b => b.ScriptSourceElement, p => p.ScriptSourceElement, (b, e) => b.AddScriptSourceElement(e), Allow.Nothing, (b, d) => b.ScriptSourceElement = d, Allow.UnsafeInline, b => b.ScriptSourceElement = null);
        await AssertBuilder(b => b.StyleSource, p => p.StyleSource, (b, e) => b.AddStyleSource(e), Allow.Nothing, (b, d) => b.StyleSource = d, Allow.Self, b => b.StyleSource = null);
        await AssertBuilder(b => b.StyleSourceAttribute, p => p.StyleSourceAttribute, (b, e) => b.AddStyleSourceAttribute(e), Allow.Nothing, (b, d) => b.StyleSourceAttribute = d, Allow.UnsafeInline, b => b.StyleSourceAttribute = null);
        await AssertBuilder(b => b.StyleSourceElement, p => p.StyleSourceElement, (b, e) => b.AddStyleSourceElement(e), Allow.Nothing, (b, d) => b.StyleSourceElement = d, Allow.UnsafeInline, b => b.StyleSourceElement = null);
        await AssertBuilder(b => b.WorkerSource, p => p.WorkerSource, (b, e) => b.AddWorkerSource(e), Allow.Nothing, (b, d) => b.WorkerSource = d, Allow.Self, b => b.WorkerSource = null);
    }

    [Test]
    public async Task CanUseConvenienceMethods_ForUpgradeInsecureRequests()
    {
        builder.SetUpgradeInsecureRequests();
        await Assert.That(builder.UpgradeInsecureRequests).IsNotNull();
        await AssertDirectiveSet(p => p.UpgradeInsecureRequests);

        builder.SetUpgradeInsecureRequests(false);
        await Assert.That(builder.UpgradeInsecureRequests).IsNull();
        await AssertDirectiveNotSet(p => p.UpgradeInsecureRequests);

        builder.UpgradeInsecureRequests = new UpgradeInsecureRequestsDirective();
        await AssertDirectiveSet(p => p.UpgradeInsecureRequests);

        builder.UpgradeInsecureRequests = null;
        await AssertDirectiveNotSet(p => p.UpgradeInsecureRequests);
    }

    [Test]
    public async Task CanUseConvenienceMethods_ForReportTo()
    {
        builder.SendReportsTo("/csp");
        await AssertDirectiveSet(
            p => p.ReportTo,
            async reportTo => await Assert.That(reportTo.Url).IsEqualTo("/csp"),
            async reportTo => await Assert.That(reportTo.EndpointName).IsEqualTo("csp-reports"));

        builder.SendReportsTo("/yeet", "my-endpoint");
        await AssertDirectiveSet(
            p => p.ReportTo,
            async reportTo => await Assert.That(reportTo.Url).IsEqualTo("/yeet"),
            async reportTo => await Assert.That(reportTo.EndpointName).IsEqualTo("my-endpoint"));

        builder.SendReportsToLocal();
        await AssertDirectiveSet(
            p => p.ReportTo,
            async reportTo => await Assert.That(reportTo.Url).IsEqualTo(ReportToDirective.LocalEndpointUri),
            async reportTo => await Assert.That(reportTo.EndpointName).IsEqualTo("csp-reports"));

        builder.SendReportsToLocal("my-endpoint");
        await AssertDirectiveSet(
            p => p.ReportTo,
            async reportTo => await Assert.That(reportTo.Url).IsEqualTo(ReportToDirective.LocalEndpointUri),
            async reportTo => await Assert.That(reportTo.EndpointName).IsEqualTo("my-endpoint"));

        builder.ReportTo = null;
        await Assert.That(builder.ReportTo).IsNull();
        await AssertDirectiveNotSet(p => p.ReportTo);

        builder.ReportTo = new ReportToDirective("/csp", "reports");
        await Assert.That(builder.ReportTo).IsNotNull();
        await AssertDirectiveSet(
            p => p.ReportTo,
            async reportTo => await Assert.That(reportTo.Url).IsEqualTo("/csp"),
            async reportTo => await Assert.That(reportTo.EndpointName).IsEqualTo("reports"));
    }

    [Test]
    public async Task CanSetReportOnly()
    {
        await Assert.That(builder.Build().ReportOnly).IsFalse();

        builder.ReportOnly();
        await Assert.That(builder.Build().ReportOnly).IsTrue();

        builder.ReportOnly(false);
        await Assert.That(builder.Build().ReportOnly).IsFalse();
    }

    private async Task AssertDirectiveNotSet<T>(Func<ContentSecurityPolicyDefinition, T?> directive) where T : Directive
    {
        var policy = builder.Build();
        var actualDirective = directive(policy);

        await Assert.That(actualDirective).IsNull().Because(typeof(T).Name);
    }

    private async Task AssertDirectiveSet<T>(Func<ContentSecurityPolicyDefinition, T?> directive, params Func<T, Task>[] assertions) where T : Directive
    {
        var policy = builder.Build();
        var actualDirective = directive(policy);

        await Assert.That(actualDirective).IsNotNull().Because(typeof(T).Name);

        foreach (var assertion in assertions)
        {
            await assertion(actualDirective);
        }
    }

    private async Task AssertDirective<T>(Func<ContentSecurityPolicyDefinition, T?> directive, params IEnumerable<ISourceExpression<T>> expectedExpressions) where T : FetchDirective<T>
    {
        var policy = builder.Build();
        var actualDirective = directive(policy);

        await Assert.That(actualDirective?.Expressions).IsEquivalentTo(expectedExpressions).Because(typeof(T).Name);
    }

    private async Task AssertBuilder<T>(
        Func<ContentSecurityPolicyBuilder, T?> directiveInBuilder,
        Func<ContentSecurityPolicyDefinition, T?> directiveInDefinition,
        Action<ContentSecurityPolicyBuilder, ISourceExpression<T>> setExpressions,
        ISourceExpression<T> firstExpression,
        Action<ContentSecurityPolicyBuilder, T> setDirective,
        ISourceExpression<T> secondExpression,
        Action<ContentSecurityPolicyBuilder> setNull) where T : FetchDirective<T>, new()
    {
        await AssertDirectiveNotSet(directiveInDefinition);

        setExpressions(builder, firstExpression);
        await AssertDirective(directiveInDefinition, firstExpression);
        await Assert.That(directiveInBuilder(builder)?.Expressions).IsEquivalentTo([firstExpression]);

        setDirective(builder, [secondExpression]);
        await AssertDirective(directiveInDefinition, secondExpression);
        await Assert.That(directiveInBuilder(builder)?.Expressions).IsEquivalentTo([secondExpression]);

        setNull(builder);
        await AssertDirectiveNotSet(directiveInDefinition);
    }
}
