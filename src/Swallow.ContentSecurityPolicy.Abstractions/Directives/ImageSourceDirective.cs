namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class ImageSourceDirective() : FetchDirective<ImageSourceDirective>(Name)
{
    public new const string Name = "image-src";
}
