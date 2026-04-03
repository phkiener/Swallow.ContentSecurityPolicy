using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Http;

public sealed class ContentSecurityPolicyFeature()
{
    public ContentSecurityPolicyFeature(Abstractions.ContentSecurityPolicy policy) : this()
    {
        Current = policy;
    }

    public Abstractions.ContentSecurityPolicy? Current { get; set; }

    public string? ScriptNonce => GetNonce(static c => c.ScriptSourceElement, static c => c.ScriptSource, static c => c.DefaultSource);
    public string? StyleNonce => GetNonce(static c => c.StyleSourceElement, static c => c.StyleSource, static c => c.DefaultSource);

    private string? GetNonce(params Func<Abstractions.ContentSecurityPolicy, FetchDirective?>[] directives)
    {
        if (Current is null)
        {
            return null;
        }

        foreach (var directive in directives)
        {
            var availableDirective = directive(Current);
            if (availableDirective is not null)
            {
                return availableDirective.Expressions.OfType<Nonce>().LastOrDefault()?.NonceValue;
            }
        }

        return null;
    }
}
