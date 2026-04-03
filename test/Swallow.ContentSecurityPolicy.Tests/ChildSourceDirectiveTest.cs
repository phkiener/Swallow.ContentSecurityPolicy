using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

[InheritsTests]
public sealed class ChildSourceDirectiveTest : FetchDirectiveTestBase<ChildSourceDirective>
{
    protected override string Name => "child-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<ChildSourceDirective> expression)
        => policy.ChildSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<ChildSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
