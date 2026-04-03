using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Swallow.ContentSecurityPolicy.Http;

namespace Swallow.ContentSecurityPolicy.Tests.Framework;

public static class TestUtils
{
    public static async Task AssertContentSecurityPolicyAsync(Abstractions.ContentSecurityPolicy policy, string expedtedHeader)
    {
        var generatedHeader = GetWrittenHeader(HeaderNames.ContentSecurityPolicy, policy);
        await Assert.That(generatedHeader).IsEqualTo(expedtedHeader);
    }

    public static string? GetCspHeader(Abstractions.ContentSecurityPolicy policy, string nonce = "NONCE")
    {
        return GetWrittenHeader(HeaderNames.ContentSecurityPolicy, policy, nonce);
    }

    private static string? GetWrittenHeader(string headerName, Abstractions.ContentSecurityPolicy policy, string nonce = "NONCE")
    {
        var feature = new ContentSecurityPolicyFeature(nonce) { Policy = policy };
        var headers = new HeaderDictionary();
        feature.SetHeader(headers, "/REPORT");

        return headers[headerName].FirstOrDefault();
    }
}
