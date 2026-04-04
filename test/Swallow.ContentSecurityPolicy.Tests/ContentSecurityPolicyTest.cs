using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Tests;

public sealed class ContentSecurityPolicyTest
{
    [Test]
    public async Task SkipsExpressions_WhenStrictDynamicIsSet()
    {
        var policy = new Abstractions.ContentSecurityPolicy
        {
            DefaultSource =
            [
                new HostSource("https://localhost:80/"),
                new SchemeSource("http:"),
                Self.Instance,
                UnsafeInline.Instance,
                StrictDynamic.Instance
            ]
        };

        await Assert.That(policy.DefaultSource.Expressions).IsEquivalentTo(new List<ISourceExpression> { StrictDynamic.Instance});
    }

    [Test]
    [MethodDataSource(nameof(AccessHelpers))]
    public async Task AccessHelpers_ReturnSpecificDirectives(TestCase testCase)
    {
        var policy = new Abstractions.ContentSecurityPolicy();
        await Assert.That(testCase.Directive(policy)).IsNull();

        testCase.SetDirective(policy);
        await Assert.That(testCase.Directive(policy)).IsNotNull();

        testCase.ClearDirective(policy);
        await Assert.That(testCase.Directive(policy)).IsNull();
    }

    public static IEnumerable<TestCase> AccessHelpers()
    {
        yield return new(p => p.BaseUri, p => p.BaseUri = [], p => p.BaseUri = null);
        yield return new(p => p.ChildSource, p => p.ChildSource = [], p => p.ChildSource = null);
        yield return new(p => p.ConnectSource, p => p.ConnectSource = [], p => p.ConnectSource = null);
        yield return new(p => p.DefaultSource, p => p.DefaultSource = [], p => p.DefaultSource = null);
        yield return new(p => p.FontSource, p => p.FontSource = [], p => p.FontSource = null);
        yield return new(p => p.FormAction, p => p.FormAction = [], p => p.FormAction = null);
        yield return new(p => p.FrameAncestors, p => p.FrameAncestors = [], p => p.FrameAncestors = null);
        yield return new(p => p.FrameSource, p => p.FrameSource = [], p => p.FrameSource = null);
        yield return new(p => p.ImageSource, p => p.ImageSource = [], p => p.ImageSource = null);
        yield return new(p => p.ManifestSource, p => p.ManifestSource = [], p => p.ManifestSource = null);
        yield return new(p => p.MediaSource, p => p.MediaSource = [], p => p.MediaSource = null);
        yield return new(p => p.ObjectSource, p => p.ObjectSource = [], p => p.ObjectSource = null);
        yield return new(p => p.ScriptSource, p => p.ScriptSource = [], p => p.ScriptSource = null);
        yield return new(p => p.ScriptSourceAttribute, p => p.ScriptSourceAttribute = [], p => p.ScriptSourceAttribute = null);
        yield return new(p => p.ScriptSourceElement, p => p.ScriptSourceElement = [], p => p.ScriptSourceElement = null);
        yield return new(p => p.StyleSource, p => p.StyleSource = [], p => p.StyleSource = null);
        yield return new(p => p.StyleSourceAttribute, p => p.StyleSourceAttribute = [], p => p.StyleSourceAttribute = null);
        yield return new(p => p.StyleSourceElement, p => p.StyleSourceElement = [], p => p.StyleSourceElement = null);
        yield return new(p => p.UpgradeInsecureRequests, p => p.UpgradeInsecureRequests = UpgradeInsecureRequestsDirective.Instance, p => p.UpgradeInsecureRequests = null);
        yield return new(p => p.WorkerSource, p => p.WorkerSource = [], p => p.WorkerSource = null);
    }

    public sealed record TestCase(
        Func<Abstractions.ContentSecurityPolicy, Directive?> Directive,
        Action<Abstractions.ContentSecurityPolicy> SetDirective,
        Action<Abstractions.ContentSecurityPolicy> ClearDirective);
}
