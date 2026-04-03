using Microsoft.AspNetCore.Http;
using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Http;

/// <summary>
/// A feature for the <see cref="HttpContext"/> that contains the <see cref="Abstractions.ContentSecurityPolicy"/> and
/// a nonce to use in related expressions.
/// </summary>
/// <param name="nonce">The nonce that will be used in expressions.</param>
public sealed class ContentSecurityPolicyFeature(string nonce)
{
    /// <summary>
    /// The currently active policy, if any.
    /// </summary>
    public Abstractions.ContentSecurityPolicy? Current { get; set; }

    /// <summary>
    /// The nonce that will be used in expressions.
    /// </summary>
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
