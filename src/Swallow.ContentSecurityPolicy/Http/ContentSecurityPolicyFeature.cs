using Microsoft.AspNetCore.Http;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Http;

public sealed class ContentSecurityPolicyFeature(string nonce)
{
    public Abstractions.ContentSecurityPolicy? Current { get; set; }

    public string Nonce { get; set; } = nonce;

    internal void SetHeader(IHeaderDictionary headerDictionary)
    {
        if (Current is null)
        {
            return;
        }

        var directives = Current.Directives.Select(AsHeaderValue);
        headerDictionary.ContentSecurityPolicy = string.Join("; ", directives);
    }

    private string AsHeaderValue(Directive directive)
    {
        var expressions = directive.Expressions.Select(e => e is Nonce ? string.Format(e.Value, Nonce) : e.Value).ToArray();

        return directive.Expressions.Any()
            ? $"{directive.Name} {string.Join(" ", expressions)}"
            : directive.Name;
    }
}
