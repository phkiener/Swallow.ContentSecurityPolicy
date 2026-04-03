using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow resources using a given scheme.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#scheme-source">scheme-source on MDN</seealso>
/// <param name="scheme">The scheme to allow.</param>
public sealed class SchemeSource(string scheme) : SourceExpression,
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
    /// <inheritdoc />
    public override string Value => scheme.EndsWith(":", StringComparison.OrdinalIgnoreCase) ? scheme : $"{scheme}:";
}
