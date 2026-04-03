namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public class StyleSourceDirective() : FetchDirective<StyleSourceDirective>(Name)
{
    public new const string Name = "style-src";
}
