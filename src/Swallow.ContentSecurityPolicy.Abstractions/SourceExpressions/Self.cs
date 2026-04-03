using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow resources from the current origin.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#self">self on MDN</seealso>
public sealed class Self : SourceExpression,
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
    /// A shared instance of the <see cref="Self"/> expression.
    /// </summary>
    public static readonly Self Instance = new();

    /// <inheritdoc />
    public override string Value => "'self'";
}
