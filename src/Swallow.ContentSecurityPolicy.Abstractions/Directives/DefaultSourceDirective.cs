namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class DefaultSourceDirective() : FetchDirective<DefaultSourceDirective>(Name)
{
    public new const string Name = "default-src";
}
