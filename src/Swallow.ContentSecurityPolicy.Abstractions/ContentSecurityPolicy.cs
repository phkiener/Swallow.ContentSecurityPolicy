using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions;

public sealed class ContentSecurityPolicy
{
    private readonly Dictionary<string, Directive> directives = [];

    public IEnumerable<Directive> Directives => directives.Values.AsEnumerable();

    public DefaultSourceDirective? DefaultSource
    {
        get => GetSpecific<DefaultSourceDirective>(DefaultSourceDirective.Name);
        set => SetOrRemove(DefaultSourceDirective.Name, value);
    }

    public ChildSourceDirective? ChildSource
    {
        get => GetSpecific<ChildSourceDirective>(ChildSourceDirective.Name);
        set => SetOrRemove(ChildSourceDirective.Name, value);
    }

    public ConnectSourceDirective? ConnectSource
    {
        get => GetSpecific<ConnectSourceDirective>(ConnectSourceDirective.Name);
        set => SetOrRemove(ConnectSourceDirective.Name, value);
    }

    public FontSourceDirective? FontSource
    {
        get => GetSpecific<FontSourceDirective>(FontSourceDirective.Name);
        set => SetOrRemove(FontSourceDirective.Name, value);
    }

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
