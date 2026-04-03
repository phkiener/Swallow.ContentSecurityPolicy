using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

[InheritsTests]
public sealed class FrameSourceDirectiveTest : FetchDirectiveTestBase<FrameSourceDirective>
{
    protected override string Name => "frame-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<FrameSourceDirective> expression)
        => policy.FrameSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<FrameSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
