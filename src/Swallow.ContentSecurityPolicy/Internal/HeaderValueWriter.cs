using Microsoft.AspNetCore.Http;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Internal;

internal static class HeaderValueWriter
{
    private const string CspHeaderName = "Content-Security-Policy";

    public static void SetHeader(IHeaderDictionary headers, Abstractions.ContentSecurityPolicy value, string nonce)
    {
        var headerValue = AsHeader(value, nonce);
        headers[CspHeaderName] = headerValue;
    }

    private static string AsHeader(Abstractions.ContentSecurityPolicy contentSecurityPolicy, string nonce)
    {
        var directives = contentSecurityPolicy.Directives.Select(d => AsHeaderValue(d, nonce));
        return string.Join("; ", directives);
    }

    private static string AsHeaderValue(Directive directive, string nonce)
    {
        return directive.Expressions.Any()
            ? $"{directive.Name} {string.Join(" ", directive.Expressions.Select(e => e is Nonce ? string.Format(e.Value, nonce) : e.Value))}"
            : directive.Name;
    }
}
