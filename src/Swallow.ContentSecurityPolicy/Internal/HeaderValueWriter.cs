using Microsoft.AspNetCore.Http;
using Swallow.ContentSecurityPolicy.Abstractions;

namespace Swallow.ContentSecurityPolicy.Internal;

internal static class HeaderValueWriter
{
    private const string CspHeaderName = "Content-Security-Policy";

    public static void SetHeader(IHeaderDictionary headers, Abstractions.ContentSecurityPolicy value)
    {
        var headerValue = AsHeader(value);
        headers[CspHeaderName] = headerValue;
    }

    private static string AsHeader(Abstractions.ContentSecurityPolicy contentSecurityPolicy)
    {
        var directives = contentSecurityPolicy.Directives.Select(AsHeaderValue);
        return string.Join("; ", directives);
    }

    private static string AsHeaderValue(Directive directive)
    {
        return directive.Expressions.Any()
            ? $"{directive.Name} {string.Join(" ", directive.Expressions.Select(static e => e.Value))}"
            : directive.Name;
    }
}
