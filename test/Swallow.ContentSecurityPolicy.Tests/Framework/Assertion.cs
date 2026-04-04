using Microsoft.Net.Http.Headers;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Http;

namespace Swallow.ContentSecurityPolicy.Tests.Framework;

public static class Assertion
{
    public const string TestNonce = "NONCE";

    public static async Task ContentSecurityPolicyAsync(Action<Abstractions.ContentSecurityPolicy> configure, string expedtedHeader)
    {
        var policy = new Abstractions.ContentSecurityPolicy();
        configure(policy);

        var generatedHeader = GetWrittenHeader(HeaderNames.ContentSecurityPolicy, policy);
        await Assert.That(generatedHeader).IsEqualTo(expedtedHeader);
    }

    public static async Task ApplicableTypes<T>(IEnumerable<ITestCase<IAppliesTo<T>>> testCases) where T : Directive
    {
        var availableTypes = typeof(T).Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IAppliesTo<T>)))
            .ToList();

        await Assert.That(availableTypes).IsEquivalentTo(testCases.Select(static t => t.Type));

    }

    private static string? GetWrittenHeader(string headerName, Abstractions.ContentSecurityPolicy policy)
    {
        var feature = new ContentSecurityPolicyFeature(TestNonce) { Policy = policy };
        var headers = new HeaderDictionary();
        feature.SetHeader(headers, "/REPORT");

        return headers[headerName].FirstOrDefault();
    }
}
