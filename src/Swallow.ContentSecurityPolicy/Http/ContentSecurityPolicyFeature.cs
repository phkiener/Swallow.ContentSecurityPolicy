namespace Swallow.ContentSecurityPolicy.Http;

public sealed class ContentSecurityPolicyFeature()
{
    public Abstractions.ContentSecurityPolicy? Current { get; set; }

    public string Nonce { get; set; } = Guid.NewGuid().ToString("D");
}
