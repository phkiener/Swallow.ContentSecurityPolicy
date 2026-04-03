namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class FrameSourceDirective() : FetchDirective<FrameSourceDirective>(Name)
{
    public new const string Name = "frame-src";
}
