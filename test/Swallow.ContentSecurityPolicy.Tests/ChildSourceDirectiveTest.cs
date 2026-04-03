using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

public sealed class ChildSourceDirectiveTest
{
    [Test]
    public async Task AcceptsConfiguredExpressions()
    {
        var expressionTypes = typeof(ChildSourceDirective).Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IAppliesTo<ChildSourceDirective>)))
            .ToList();

        await Assert.That(expressionTypes).IsEquivalentTo([typeof(HostSource), typeof(SchemeSource), typeof(Self), typeof(DenyAll)]);
    }

    [Test]
    public async Task WritesExpectedHeader()
    {
        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { ChildSource = [new HostSource("https://localhost:80/base")] },
            "child-src https://localhost:80/base");

        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { ChildSource = [new SchemeSource("https"), new SchemeSource("http:")] },
            "child-src https: http:");

        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { ChildSource = [Self.Instance] },
            "child-src 'self'");

        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { ChildSource = [] },
            "child-src 'none'");

        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { ChildSource = [DenyAll.Instance] },
            "child-src 'none'");
    }
}
