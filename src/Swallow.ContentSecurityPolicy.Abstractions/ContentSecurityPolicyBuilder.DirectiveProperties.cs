using Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions;

public partial class ContentSecurityPolicyBuilder
{
    // TODO: This part should be source generated.

    /// <summary>
    /// The configured <see cref="BaseUriDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public BaseUriDirective? BaseUri
    {
        get => GetDirective<BaseUriDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="ChildSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ChildSourceDirective? ChildSource
    {
        get => GetDirective<ChildSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="ConnectSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ConnectSourceDirective? ConnectSource
    {
        get => GetDirective<ConnectSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="DefaultSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public DefaultSourceDirective? DefaultSource
    {
        get => GetDirective<DefaultSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="FontSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public FontSourceDirective? FontSource
    {
        get => GetDirective<FontSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="FormActionDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public FormActionDirective? FormAction
    {
        get => GetDirective<FormActionDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="FrameAncestorsDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public FrameAncestorsDirective? FrameAncestors
    {
        get => GetDirective<FrameAncestorsDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="FrameSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public FrameSourceDirective? FrameSource
    {
        get => GetDirective<FrameSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="ImageSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ImageSourceDirective? ImageSource
    {
        get => GetDirective<ImageSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="ManifestSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ManifestSourceDirective? ManifestSource
    {
        get => GetDirective<ManifestSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="MediaSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public MediaSourceDirective? MediaSource
    {
        get => GetDirective<MediaSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="ObjectSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ObjectSourceDirective? ObjectSource
    {
        get => GetDirective<ObjectSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="ReportToDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ReportToDirective? ReportTo
    {
        get => GetDirective<ReportToDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="ScriptSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ScriptSourceDirective? ScriptSource
    {
        get => GetDirective<ScriptSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="ScriptSourceAttributeDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ScriptSourceAttributeDirective? ScriptSourceAttribute
    {
        get => GetDirective<ScriptSourceAttributeDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="ScriptSourceElementDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ScriptSourceElementDirective? ScriptSourceElement
    {
        get => GetDirective<ScriptSourceElementDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="StyleSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public StyleSourceDirective? StyleSource
    {
        get => GetDirective<StyleSourceDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="StyleSourceAttributeDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public StyleSourceAttributeDirective? StyleSourceAttribute
    {
        get => GetDirective<StyleSourceAttributeDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="StyleSourceElementDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public StyleSourceElementDirective? StyleSourceElement
    {
        get => GetDirective<StyleSourceElementDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="UpgradeInsecureRequestsDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public UpgradeInsecureRequestsDirective? UpgradeInsecureRequests
    {
        get => GetDirective<UpgradeInsecureRequestsDirective>();
        set => SetDirective(value);
    }

    /// <summary>
    /// The configured <see cref="WorkerSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public WorkerSourceDirective? WorkerSource
    {
        get => GetDirective<WorkerSourceDirective>();
        set => SetDirective(value);
    }
}
