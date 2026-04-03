using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

[InheritsTests]
public sealed class FrameAncestorsDirectiveTest : FetchDirectiveTestBase<FrameAncestorsDirective>
{
    protected override string Name => "frame-ancestors";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<FrameAncestorsDirective> expression)
        => policy.FrameAncestors = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<FrameAncestorsDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
