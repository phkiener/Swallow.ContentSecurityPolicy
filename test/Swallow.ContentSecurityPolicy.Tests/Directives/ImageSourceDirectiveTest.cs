using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests.Directives;

[InheritsTests]
public sealed class ImageSourceDirectiveTest : FetchDirectiveTestBase<ImageSourceDirective>
{
    protected override string Name => "image-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<ImageSourceDirective> expression)
        => policy.ImageSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<ImageSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
