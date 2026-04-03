namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public class ScriptSourceElementDirective() : FetchDirective<ScriptSourceElementDirective>(Name)
{
    public new const string Name = "script-src-elem";
}
