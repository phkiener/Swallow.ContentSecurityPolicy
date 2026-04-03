namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class MediaSourceDirective() : FetchDirective<MediaSourceDirective>(Name)
{
    public new const string Name = "media-src";
}
