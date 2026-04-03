using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests.Directives;

[InheritsTests]
public sealed class BaseUriDirectiveTest : FetchDirectiveTestBase<BaseUriDirective>
{
    protected override string Name => "base-uri";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<BaseUriDirective> expression)
        => policy.BaseUri = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<BaseUriDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
