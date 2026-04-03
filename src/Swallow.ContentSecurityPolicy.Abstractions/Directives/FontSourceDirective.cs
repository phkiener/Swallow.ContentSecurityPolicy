namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class FontSourceDirective() : FetchDirective<FontSourceDirective>(Name)
{
    public new const string Name = "font-src";
}
