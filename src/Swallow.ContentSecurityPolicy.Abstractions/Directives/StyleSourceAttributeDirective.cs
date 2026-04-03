namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public class StyleSourceAttributeDirective() : FetchDirective<StyleSourceAttributeDirective>(Name)
{
    public new const string Name = "style-src-attr";
}
