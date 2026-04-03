using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests.Directives;

[InheritsTests]
public sealed class MediaSourceDirectiveTest : FetchDirectiveTestBase<MediaSourceDirective>
{
    protected override string Name => "media-src";

    protected override void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<MediaSourceDirective> expression)
        => policy.MediaSource = [expression];

    protected override IEnumerable<ITestCase<IAppliesTo<MediaSourceDirective>>> EnumerateTestCases()
    {
        yield return TestCases.For<DenyAll>();
        yield return TestCases.For<HostSource>();
        yield return TestCases.For<SchemeSource>();
        yield return TestCases.For<Self>();
    }
}
