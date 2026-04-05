using Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to <c>'none'</c>, denying all
/// resources.
/// </summary>
/// <remarks>
/// This should be called <c>None</c>, but that's just way too similar to
/// <see cref="Nonce"/>. To prevent any unfortunate typos, this one has a
/// different name.
/// </remarks>
public sealed record DenyAll :
    ISourceExpression<BaseUriDirective>,
    ISourceExpression<ChildSourceDirective>,
    ISourceExpression<ConnectSourceDirective>,
    ISourceExpression<DefaultSourceDirective>,
    ISourceExpression<FontSourceDirective>,
    ISourceExpression<FormActionDirective>,
    ISourceExpression<FrameAncestorsDirective>,
    ISourceExpression<FrameSourceDirective>,
    ISourceExpression<ImageSourceDirective>,
    ISourceExpression<ManifestSourceDirective>,
    ISourceExpression<MediaSourceDirective>,
    ISourceExpression<ObjectSourceDirective>,
    ISourceExpression<ScriptSourceDirective>,
    ISourceExpression<ScriptSourceAttributeDirective>,
    ISourceExpression<ScriptSourceElementDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceAttributeDirective>,
    ISourceExpression<StyleSourceElementDirective>,
    ISourceExpression<WorkerSourceDirective>;
