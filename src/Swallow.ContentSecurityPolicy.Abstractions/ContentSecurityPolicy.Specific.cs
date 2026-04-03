using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions;

public sealed partial class ContentSecurityPolicy
{
    /// <summary>
    /// Get or set the <see cref="BaseUriDirective"/>.
    /// </summary>
    public BaseUriDirective? BaseUri
    {
        get => GetSpecific<BaseUriDirective>(BaseUriDirective.Name);
        set => SetOrRemove(BaseUriDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="ChildSourceDirective"/>.
    /// </summary>
    public ChildSourceDirective? ChildSource
    {
        get => GetSpecific<ChildSourceDirective>(ChildSourceDirective.Name);
        set => SetOrRemove(ChildSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="ConnectSourceDirective"/>.
    /// </summary>
    public ConnectSourceDirective? ConnectSource
    {
        get => GetSpecific<ConnectSourceDirective>(ConnectSourceDirective.Name);
        set => SetOrRemove(ConnectSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="DefaultSourceDirective"/>.
    /// </summary>
    public DefaultSourceDirective? DefaultSource
    {
        get => GetSpecific<DefaultSourceDirective>(DefaultSourceDirective.Name);
        set => SetOrRemove(DefaultSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="FontSourceDirective"/>.
    /// </summary>
    public FontSourceDirective? FontSource
    {
        get => GetSpecific<FontSourceDirective>(FontSourceDirective.Name);
        set => SetOrRemove(FontSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="FormActionDirective"/>.
    /// </summary>
    public FormActionDirective? FormAction
    {
        get => GetSpecific<FormActionDirective>(FormActionDirective.Name);
        set => SetOrRemove(FormActionDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="FrameAncestorsDirective"/>.
    /// </summary>
    public FrameAncestorsDirective? FrameAncestors
    {
        get => GetSpecific<FrameAncestorsDirective>(FrameAncestorsDirective.Name);
        set => SetOrRemove(FrameAncestorsDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="FrameSourceDirective"/>.
    /// </summary>
    public FrameSourceDirective? FrameSource
    {
        get => GetSpecific<FrameSourceDirective>(FrameSourceDirective.Name);
        set => SetOrRemove(FrameSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="ImageSourceDirective"/>.
    /// </summary>
    public ImageSourceDirective? ImageSource
    {
        get => GetSpecific<ImageSourceDirective>(ImageSourceDirective.Name);
        set => SetOrRemove(ImageSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="ManifestSourceDirective"/>.
    /// </summary>
    public ManifestSourceDirective? ManifestSource
    {
        get => GetSpecific<ManifestSourceDirective>(ManifestSourceDirective.Name);
        set => SetOrRemove(ManifestSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="MediaSourceDirective"/>.
    /// </summary>
    public MediaSourceDirective? MediaSource
    {
        get => GetSpecific<MediaSourceDirective>(MediaSourceDirective.Name);
        set => SetOrRemove(MediaSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="ObjectSourceDirective"/>.
    /// </summary>
    public ObjectSourceDirective? ObjectSource
    {
        get => GetSpecific<ObjectSourceDirective>(ObjectSourceDirective.Name);
        set => SetOrRemove(ObjectSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="ScriptSourceDirective"/>.
    /// </summary>
    public ScriptSourceDirective? ScriptSource
    {
        get => GetSpecific<ScriptSourceDirective>(ScriptSourceDirective.Name);
        set => SetOrRemove(ScriptSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="ScriptSourceAttributeDirective"/>.
    /// </summary>
    public ScriptSourceAttributeDirective? ScriptSourceAttribute
    {
        get => GetSpecific<ScriptSourceAttributeDirective>(ScriptSourceAttributeDirective.Name);
        set => SetOrRemove(ScriptSourceAttributeDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="ScriptSourceElementDirective"/>.
    /// </summary>
    public ScriptSourceElementDirective? ScriptSourceElement
    {
        get => GetSpecific<ScriptSourceElementDirective>(ScriptSourceElementDirective.Name);
        set => SetOrRemove(ScriptSourceElementDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="StyleSourceDirective"/>.
    /// </summary>
    public StyleSourceDirective? StyleSource
    {
        get => GetSpecific<StyleSourceDirective>(StyleSourceDirective.Name);
        set => SetOrRemove(StyleSourceDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="StyleSourceAttributeDirective"/>.
    /// </summary>
    public StyleSourceAttributeDirective? StyleSourceAttribute
    {
        get => GetSpecific<StyleSourceAttributeDirective>(StyleSourceAttributeDirective.Name);
        set => SetOrRemove(StyleSourceAttributeDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="StyleSourceElementDirective"/>.
    /// </summary>
    public StyleSourceElementDirective? StyleSourceElement
    {
        get => GetSpecific<StyleSourceElementDirective>(StyleSourceElementDirective.Name);
        set => SetOrRemove(StyleSourceElementDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="UpgradeInsecureRequestsDirective"/>.
    /// </summary>
    public UpgradeInsecureRequestsDirective? UpgradeInsecureRequests
    {
        get => GetSpecific<UpgradeInsecureRequestsDirective>(UpgradeInsecureRequestsDirective.Name);
        set => SetOrRemove(UpgradeInsecureRequestsDirective.Name, value);
    }

    /// <summary>
    /// Get or set the <see cref="WorkerSourceDirective"/>.
    /// </summary>
    public WorkerSourceDirective? WorkerSource
    {
        get => GetSpecific<WorkerSourceDirective>(WorkerSourceDirective.Name);
        set => SetOrRemove(WorkerSourceDirective.Name, value);
    }
}
