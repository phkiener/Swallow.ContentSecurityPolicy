using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests.Directives;

[InheritsTests]
public sealed class StyleSourceAttributeDirectiveTest : FetchDirectiveTestBase<StyleSourceAttributeDirective>
{
    protected override string Name => "style-src-attr";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<StyleSourceAttributeDirective> expression)
        => policy.StyleSourceAttribute = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<StyleSourceAttributeDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<ReportSample>();
        yield return TestCases.For<UnsafeHashes>();
        yield return TestCases.For<UnsafeInline>();
    }
}
