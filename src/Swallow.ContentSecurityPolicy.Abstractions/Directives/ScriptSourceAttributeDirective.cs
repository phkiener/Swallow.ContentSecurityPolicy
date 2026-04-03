namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public class ScriptSourceAttributeDirective() : FetchDirective<ScriptSourceAttributeDirective>(Name)
{
    public new const string Name = "script-src-attr";
}
