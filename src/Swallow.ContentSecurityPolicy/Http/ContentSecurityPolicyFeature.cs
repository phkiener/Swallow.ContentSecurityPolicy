using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Http;

public sealed class ContentSecurityPolicyFeature
{
    public ContentSecurityPolicyFeature(Abstractions.ContentSecurityPolicy policy)
    {
        Current = policy;
        Nonce = policy.Directives.OfType<FetchDirective>()
            .SelectMany(static d => d.Expressions)
            .OfType<Nonce>()
            .Select(static n => n.NonceValue)
            .FirstOrDefault();
    }

    public string? Nonce { get; }

    public Abstractions.ContentSecurityPolicy Current { get; }
}
