using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

[InheritsTests]
public sealed class FormActionDirectiveTest : FetchDirectiveTestBase<FormActionDirective>
{
    protected override string Name => "form-action";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<FormActionDirective> expression)
        => policy.FormAction = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<FormActionDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
