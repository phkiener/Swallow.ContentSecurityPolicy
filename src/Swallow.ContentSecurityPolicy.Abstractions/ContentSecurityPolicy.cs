namespace Swallow.ContentSecurityPolicy.Abstractions;

public sealed partial class ContentSecurityPolicy
{
    private readonly Dictionary<string, Directive> directives = [];

    public IEnumerable<Directive> Directives => directives.Values.AsEnumerable();

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
