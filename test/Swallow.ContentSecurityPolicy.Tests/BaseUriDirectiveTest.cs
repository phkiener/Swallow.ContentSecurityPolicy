using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

public sealed class BaseUriDirectiveTest
{
    [Test]
    public async Task AcceptsConfiguredExpressions()
    {
        var expressionTypes = typeof(BaseUriDirective).Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IAppliesTo<BaseUriDirective>)))
            .ToList();

        await Assert.That(expressionTypes).IsEquivalentTo([typeof(HostSource), typeof(SchemeSource), typeof(Self), typeof(DenyAll)]);
    }

    [Test]
    public async Task WritesExpectedHeader()
    {
        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { BaseUri = [new HostSource("https://localhost:80/base")] },
            "base-uri https://localhost:80/base");

        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { BaseUri = [new SchemeSource("https"), new SchemeSource("http:")] },
            "base-uri https: http:");

        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { BaseUri = [Self.Instance] },
            "base-uri 'self'");

        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { BaseUri = [] },
            "base-uri 'none'");

        await TestUtils.AssertContentSecurityPolicyAsync(
            new Abstractions.ContentSecurityPolicy { BaseUri = [DenyAll.Instance] },
            "base-uri 'none'");
    }
}
