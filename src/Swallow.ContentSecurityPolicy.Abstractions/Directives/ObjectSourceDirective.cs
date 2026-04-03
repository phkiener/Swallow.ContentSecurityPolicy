namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class ObjectSourceDirective() : FetchDirective<ObjectSourceDirective>(Name)
{
    public new const string Name = "object-src";
}
