namespace Swallow.ContentSecurityPolicy.Abstractions;

/// <summary>
/// A Content Security Policy that will control which external resources are fetched.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#directives">CSP on MDN</seealso>
public sealed partial class ContentSecurityPolicy
{
    private readonly Dictionary<string, Directive> directives = [];

    /// <summary>
    /// The <see cref="Directive"/>s that this policy contains.
    /// </summary>
    public IEnumerable<Directive> Directives => directives.Values.AsEnumerable();

    /// <summary>
    /// Add a <see cref="Directive"/> to the list of directives.
    /// </summary>
    /// <remarks>
    /// If a directive with the same name already exists, it will be overwritten.
    /// </remarks>
    /// <param name="directive">The <see cref="Directive"/> to add.</param>
    public void Add(Directive directive)
    {
        directives[directive.Name] = directive;
    }

    private T? GetSpecific<T>(string key) where T : class
    {
        return directives.GetValueOrDefault(key) as T;
    }

    private void SetOrRemove(string key, Directive? value)
    {
        if (value is null)
        {
            directives.Remove(key);
        }
        else
        {
            directives[key] = value;
        }
    }
}
