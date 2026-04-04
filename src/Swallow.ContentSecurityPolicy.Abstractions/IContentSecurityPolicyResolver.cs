namespace Swallow.ContentSecurityPolicy.Abstractions;

public interface IContentSecurityPolicyResolver
{
    ContentSecurityPolicy? GetDefaultPolicy();

    ContentSecurityPolicy? GetPolicy(string name);
}
