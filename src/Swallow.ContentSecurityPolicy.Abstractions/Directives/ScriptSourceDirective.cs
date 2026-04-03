namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public class ScriptSourceDirective() : FetchDirective<ScriptSourceDirective>(Name)
{
    public new const string Name = "script-src";
}
