using Swallow.ContentSecurityPolicy.Abstractions.V2.Model;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2;

/// <summary>
/// A Content Security Policy (CSP) that controls which additional resources
/// (e.g. stylesheets and scripts) may be loaded and/or executed.
/// </summary>
/// <param name="directives">The directives that this policy contains.</param>
/// <param name="reportOnly">Whether this policy should only report the violations or actually enforce them (which is the default).</param>
public sealed class ContentSecurityPolicyDefinition(IEnumerable<Directive> directives, bool reportOnly = false)
{
    /// <summary>
    /// Whether this policy should only report the violations or actually enforce them (which is the default).
    /// </summary>
    public bool ReportOnly { get; } = reportOnly;

    /// <summary>
    /// Enumerates all configured directives.
    /// </summary>
    public IReadOnlyList<Directive> Directives { get; } = new List<Directive>(directives).AsReadOnly();

    /// <summary>
    /// The configured <see cref="BaseUriDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public BaseUriDirective? BaseUri => GetDirective<BaseUriDirective>();

    /// <summary>
    /// The configured <see cref="ChildSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ChildSourceDirective? ChildSource => GetDirective<ChildSourceDirective>();

    /// <summary>
    /// The configured <see cref="DefaultSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public DefaultSourceDirective? DefaultSource => GetDirective<DefaultSourceDirective>();

    /// <summary>
    /// The configured <see cref="FontSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public FontSourceDirective? FontSource => GetDirective<FontSourceDirective>();

    /// <summary>
    /// The configured <see cref="FormActionDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public FormActionDirective? FormAction => GetDirective<FormActionDirective>();

    /// <summary>
    /// The configured <see cref="FrameAncestorsDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public FrameAncestorsDirective? FrameAncestors => GetDirective<FrameAncestorsDirective>();

    /// <summary>
    /// The configured <see cref="FrameSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public FrameSourceDirective? FrameSource => GetDirective<FrameSourceDirective>();

    /// <summary>
    /// The configured <see cref="ImageSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ImageSourceDirective? ImageSource => GetDirective<ImageSourceDirective>();

    /// <summary>
    /// The configured <see cref="ManifestSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ManifestSourceDirective? ManifestSource => GetDirective<ManifestSourceDirective>();

    /// <summary>
    /// The configured <see cref="MediaSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public MediaSourceDirective? MediaSource => GetDirective<MediaSourceDirective>();

    /// <summary>
    /// The configured <see cref="ObjectSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ObjectSourceDirective? ObjectSource => GetDirective<ObjectSourceDirective>();

    /// <summary>
    /// The configured <see cref="ReportToDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ReportToDirective? ReportTo => GetDirective<ReportToDirective>();

    /// <summary>
    /// The configured <see cref="ScriptSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ScriptSourceDirective? ScriptSource => GetDirective<ScriptSourceDirective>();

    /// <summary>
    /// The configured <see cref="ScriptSourceAttributeDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ScriptSourceAttributeDirective? ScriptSourceAttribute => GetDirective<ScriptSourceAttributeDirective>();

    /// <summary>
    /// The configured <see cref="ScriptSourceElementDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public ScriptSourceElementDirective? ScriptSourceElement => GetDirective<ScriptSourceElementDirective>();

    /// <summary>
    /// The configured <see cref="StyleSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public StyleSourceDirective? StyleSource => GetDirective<StyleSourceDirective>();

    /// <summary>
    /// The configured <see cref="StyleSourceAttributeDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public StyleSourceAttributeDirective? StyleSourceAttribute => GetDirective<StyleSourceAttributeDirective>();

    /// <summary>
    /// The configured <see cref="StyleSourceElementDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public StyleSourceElementDirective? StyleSourceElement => GetDirective<StyleSourceElementDirective>();

    /// <summary>
    /// The configured <see cref="UpgradeInsecureRequestsDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public UpgradeInsecureRequestsDirective? UpgradeInsecureRequests => GetDirective<UpgradeInsecureRequestsDirective>();

    /// <summary>
    /// The configured <see cref="WorkerSourceDirective"/> or <see langword="null"/> if none exists.
    /// </summary>
    public WorkerSourceDirective? WorkerSource => GetDirective<WorkerSourceDirective>();

    private T? GetDirective<T>() where T : Directive
    {
        return directives.OfType<T>().FirstOrDefault();
    }
}
