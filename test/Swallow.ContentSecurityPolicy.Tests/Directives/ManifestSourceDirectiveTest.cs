using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests.Directives;

[InheritsTests]
public sealed class ManifestSourceDirectiveTest : FetchDirectiveTestBase<ManifestSourceDirective>
{
    protected override string Name => "manifest-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<ManifestSourceDirective> expression)
        => policy.ManifestSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<ManifestSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
