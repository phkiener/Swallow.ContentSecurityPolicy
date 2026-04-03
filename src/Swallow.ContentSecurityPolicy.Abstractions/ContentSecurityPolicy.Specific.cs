using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions;

public sealed partial class ContentSecurityPolicy
{
    public BaseUriDirective? BaseUri
    {
        get => GetSpecific<BaseUriDirective>(BaseUriDirective.Name);
        set => SetOrRemove(BaseUriDirective.Name, value);
    }

    public ChildSourceDirective? ChildSource
    {
        get => GetSpecific<ChildSourceDirective>(ChildSourceDirective.Name);
        set => SetOrRemove(ChildSourceDirective.Name, value);
    }

    public ConnectSourceDirective? ConnectSource
    {
        get => GetSpecific<ConnectSourceDirective>(ConnectSourceDirective.Name);
        set => SetOrRemove(ConnectSourceDirective.Name, value);
    }

    public DefaultSourceDirective? DefaultSource
    {
        get => GetSpecific<DefaultSourceDirective>(DefaultSourceDirective.Name);
        set => SetOrRemove(DefaultSourceDirective.Name, value);
    }

    public FontSourceDirective? FontSource
    {
        get => GetSpecific<FontSourceDirective>(FontSourceDirective.Name);
        set => SetOrRemove(FontSourceDirective.Name, value);
    }

    public FormActionDirective? FormAction
    {
        get => GetSpecific<FormActionDirective>(FormActionDirective.Name);
        set => SetOrRemove(FormActionDirective.Name, value);
    }

    public FrameAncestorsDirective? FrameAncestors
    {
        get => GetSpecific<FrameAncestorsDirective>(FrameAncestorsDirective.Name);
        set => SetOrRemove(FrameAncestorsDirective.Name, value);
    }

    public FrameSourceDirective? FrameSource
    {
        get => GetSpecific<FrameSourceDirective>(FrameSourceDirective.Name);
        set => SetOrRemove(FrameSourceDirective.Name, value);
    }

    public ImageSourceDirective? ImageSource
    {
        get => GetSpecific<ImageSourceDirective>(ImageSourceDirective.Name);
        set => SetOrRemove(ImageSourceDirective.Name, value);
    }

    public ManifestSourceDirective? ManifestSource
    {
        get => GetSpecific<ManifestSourceDirective>(ManifestSourceDirective.Name);
        set => SetOrRemove(ManifestSourceDirective.Name, value);
    }

    public MediaSourceDirective? MediaSource
    {
        get => GetSpecific<MediaSourceDirective>(MediaSourceDirective.Name);
        set => SetOrRemove(MediaSourceDirective.Name, value);
    }

    public ObjectSourceDirective? ObjectSource
    {
        get => GetSpecific<ObjectSourceDirective>(ObjectSourceDirective.Name);
        set => SetOrRemove(ObjectSourceDirective.Name, value);
    }

    public ScriptSourceDirective? ScriptSource
    {
        get => GetSpecific<ScriptSourceDirective>(ScriptSourceDirective.Name);
        set => SetOrRemove(ScriptSourceDirective.Name, value);
    }

    public ScriptSourceAttributeDirective? ScriptSourceAttribute
    {
        get => GetSpecific<ScriptSourceAttributeDirective>(ScriptSourceAttributeDirective.Name);
        set => SetOrRemove(ScriptSourceAttributeDirective.Name, value);
    }

    public ScriptSourceElementDirective? ScriptSourceElement
    {
        get => GetSpecific<ScriptSourceElementDirective>(ScriptSourceElementDirective.Name);
        set => SetOrRemove(ScriptSourceElementDirective.Name, value);
    }

    public StyleSourceDirective? StyleSource
    {
        get => GetSpecific<StyleSourceDirective>(StyleSourceDirective.Name);
        set => SetOrRemove(StyleSourceDirective.Name, value);
    }

    public StyleSourceAttributeDirective? StyleSourceAttribute
    {
        get => GetSpecific<StyleSourceAttributeDirective>(StyleSourceAttributeDirective.Name);
        set => SetOrRemove(StyleSourceAttributeDirective.Name, value);
    }

    public StyleSourceElementDirective? StyleSourceElement
    {
        get => GetSpecific<StyleSourceElementDirective>(StyleSourceElementDirective.Name);
        set => SetOrRemove(StyleSourceElementDirective.Name, value);
    }

    public UpgradeInsecureRequestsDirective? UpgradeInsecureRequests
    {
        get => GetSpecific<UpgradeInsecureRequestsDirective>(UpgradeInsecureRequestsDirective.Name);
        set => SetOrRemove(UpgradeInsecureRequestsDirective.Name, value);
    }

    public WorkerSourceDirective? WorkerSource
    {
        get => GetSpecific<WorkerSourceDirective>(WorkerSourceDirective.Name);
        set => SetOrRemove(WorkerSourceDirective.Name, value);
    }
}
