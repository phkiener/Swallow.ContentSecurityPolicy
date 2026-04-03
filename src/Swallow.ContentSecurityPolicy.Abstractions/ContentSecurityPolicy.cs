using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions;

public sealed class ContentSecurityPolicy
{
    private readonly Dictionary<string, Directive> directives = [];

    public IEnumerable<Directive> Directives => directives.Values.AsEnumerable();

    public DefaultSourceDirective? DefaultSource
    {
        get => directives.GetValueOrDefault(DefaultSourceDirective.Name) as DefaultSourceDirective;
        set => SetOrRemove(DefaultSourceDirective.Name, value);
    }

    public void Add(Directive directive)
    {
        directives[directive.Name] = directive;
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
