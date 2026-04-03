using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests.Directives;

[InheritsTests]
public sealed class ObjectSourceDirectiveTest : FetchDirectiveTestBase<ObjectSourceDirective>
{
    protected override string Name => "object-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<ObjectSourceDirective> expression)
        => policy.ObjectSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<ObjectSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
