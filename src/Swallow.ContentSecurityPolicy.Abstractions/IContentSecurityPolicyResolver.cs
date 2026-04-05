namespace Swallow.ContentSecurityPolicy.Abstractions;

/// <summary>
/// Resolve a specific <see cref="ContentSecurityPolicyDefinition"/> based on
/// its name.
/// </summary>
public interface IContentSecurityPolicyResolver
{
    /// <summary>
    /// Return the default <see cref="ContentSecurityPolicyDefinition" />, if any.
    /// </summary>
    /// <returns>The default policy or <see langword="null"/> if it doesn't exist.</returns>
    ContentSecurityPolicyDefinition? DefaultPolicy();

    /// <summary>
    /// Return the <see cref="ContentSecurityPolicyDefinition" /> that was configured
    /// as <paramref name="name"/>.
    /// </summary>
    /// <param name="name">The name of the policy to return.</param>
    /// <returns>The requested policy or <see langword="null"/> if it doesn't exist.</returns>
    ContentSecurityPolicyDefinition? GetPolicy(string name);
}
