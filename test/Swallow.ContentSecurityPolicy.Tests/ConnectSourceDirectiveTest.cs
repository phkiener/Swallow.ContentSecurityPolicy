using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

[InheritsTests]
public sealed class ConnectSourceDirectiveTest : FetchDirectiveTestBase<ConnectSourceDirective>
{
    protected override string Name => "connect-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<ConnectSourceDirective> expression)
        => policy.ConnectSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<ConnectSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
