namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class BaseUriDirective() : FetchDirective<BaseUriDirective>(Name)
{
    public new const string Name = "base-uri";
}
