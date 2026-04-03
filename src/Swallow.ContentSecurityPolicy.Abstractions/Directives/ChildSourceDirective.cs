namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class ChildSourceDirective() : FetchDirective<ChildSourceDirective>(Name)
{
    public new const string Name = "child-src";
}
