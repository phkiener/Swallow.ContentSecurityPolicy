using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

[InheritsTests]
public sealed class WorkerSourceDirectiveTest : FetchDirectiveTestBase<WorkerSourceDirective>
{
    protected override string Name => "worker-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<WorkerSourceDirective> expression)
        => policy.WorkerSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<WorkerSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
