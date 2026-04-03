namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class ManifestSourceDirective() : FetchDirective<ManifestSourceDirective>(Name)
{
    public new const string Name = "manifest-src";
}
