using Swallow.ContentSecurityPolicy.Abstractions.V2.Model;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2;

/// <summary>
/// A Content Security Policy (CSP) that controls which additional resources
/// (e.g. stylesheets and scripts) may be loaded and/or executed.
/// </summary>
/// <param name="directives">The directives that this policy contains.</param>
public sealed class ContentSecurityPolicyDefinition(IEnumerable<Directive> directives)
{
    /// <summary>
    /// Enumerates all configured directives.
    /// </summary>
    public IReadOnlyList<Directive> Directives { get; } = new List<Directive>(directives).AsReadOnly();
}
