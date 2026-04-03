namespace Swallow.ContentSecurityPolicy.Internal;

internal sealed class GuidNonceGenerator : INonceGenerator
{
    public string Generate() => Guid.NewGuid().ToString("D");
}
