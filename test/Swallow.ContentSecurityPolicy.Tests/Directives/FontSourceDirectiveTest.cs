using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests.Directives;

[InheritsTests]
public sealed class FontSourceDirectiveTest : FetchDirectiveTestBase<FontSourceDirective>
{
    protected override string Name => "font-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<FontSourceDirective> expression)
        => policy.FontSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<FontSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
