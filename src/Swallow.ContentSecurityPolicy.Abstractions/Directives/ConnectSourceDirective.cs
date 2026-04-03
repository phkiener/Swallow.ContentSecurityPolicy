namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class ConnectSourceDirective() : FetchDirective<ConnectSourceDirective>(Name)
{
    public new const string Name = "connect-src";
}
