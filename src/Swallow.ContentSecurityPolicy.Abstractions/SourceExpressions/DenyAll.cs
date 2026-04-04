using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to <c>'none'</c>, denying all
/// resources.
/// </summary>
/// <remarks>
/// This should be called <c>None</c>, but that's just way too similar to
/// <see cref="Nonce"/>. To prevent any unfortunate typos, this one has a
/// different name.
/// </remarks>
public sealed class DenyAll : SourceExpression,
    IAppliesTo<BaseUriDirective>,
    IAppliesTo<ChildSourceDirective>,
    IAppliesTo<ConnectSourceDirective>,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<FontSourceDirective>,
    IAppliesTo<FormActionDirective>,
    IAppliesTo<FrameAncestorsDirective>,
    IAppliesTo<FrameSourceDirective>,
    IAppliesTo<ImageSourceDirective>,
    IAppliesTo<ManifestSourceDirective>,
    IAppliesTo<MediaSourceDirective>,
    IAppliesTo<ObjectSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>,
    IAppliesTo<WorkerSourceDirective>
{
    /// <summary>
    /// A shared instance of the <see cref="DenyAll"/> expression.
    /// </summary>
    public static readonly DenyAll Instance = new();

    /// <inheritdoc />
    public override string Value => "'none'";
}
