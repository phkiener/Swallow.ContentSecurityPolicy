using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

[InheritsTests]
public sealed class DefaultSourceDirectiveTest : FetchDirectiveTestBase<DefaultSourceDirective>
{
    protected override string Name => "default-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<DefaultSourceDirective> expression)
        => policy.DefaultSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<DefaultSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<Hash>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<InlineSpeculationRules>();
        yield return TestCases.For<Nonce>();
        yield return TestCases.For<ReportSample>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
        yield return TestCases.For<StrictDynamic>();
        yield return TestCases.For<TrustedTypesEval>();
        yield return TestCases.For<UnsafeEval>();
        yield return TestCases.For<UnsafeHashes>();
        yield return TestCases.For<UnsafeInline>();
        yield return TestCases.For<WasmUnsafeEval>();
    }
}
