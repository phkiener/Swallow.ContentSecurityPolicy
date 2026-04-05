using Swallow.ContentSecurityPolicy.Abstractions.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow resources from the current origin.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#self">self on MDN</seealso>
public sealed record Self :
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
    ISourceExpression<ScriptSourceElementDirective>,
    ISourceExpression<StyleSourceDirective>,
    ISourceExpression<StyleSourceElementDirective>,
    ISourceExpression<WorkerSourceDirective>;
