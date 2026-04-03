namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class FrameAncestorsDirective() : FetchDirective<FrameAncestorsDirective>(Name)
{
    public new const string Name = "frame-ancestors";
}
