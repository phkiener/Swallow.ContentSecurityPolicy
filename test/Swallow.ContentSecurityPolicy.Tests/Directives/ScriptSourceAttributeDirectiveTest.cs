using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests.Directives;

[InheritsTests]
public sealed class ScriptSourceAttributeDirectiveTest : FetchDirectiveTestBase<ScriptSourceAttributeDirective>
{
    protected override string Name => "script-src-attr";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<ScriptSourceAttributeDirective> expression)
        => policy.ScriptSourceAttribute = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<ScriptSourceAttributeDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<ReportSample>();
        yield return TestCases.For<UnsafeHashes>();
        yield return TestCases.For<UnsafeInline>();
    }
}
