using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Tests.Framework;

namespace Swallow.ContentSecurityPolicy.Tests;

public sealed class UpgradeInsecureRequestsDirectiveTest
{
    [Test]
    public async Task WritesExpectedHeader()
    {
        await Assertion.ContentSecurityPolicyAsync(p => p.UpgradeInsecureRequests = UpgradeInsecureRequestsDirective.Instance, "upgrade-insecure-requests");
    }
}
