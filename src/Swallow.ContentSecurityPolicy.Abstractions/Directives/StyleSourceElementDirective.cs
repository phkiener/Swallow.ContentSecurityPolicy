namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public class StyleSourceElementDirective() : FetchDirective<StyleSourceElementDirective>(Name)
{
    public new const string Name = "style-src-elem";
}
