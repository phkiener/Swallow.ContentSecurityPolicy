using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;

/// <summary>
/// Set the containing <see cref="Directive"/> to allow resources using a given scheme.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#scheme-source">scheme-source on MDN</seealso>
/// <param name="Scheme">The scheme to allow.</param>
public sealed record SchemeSource(string Scheme) :
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
    ISourceExpression<WorkerSourceDirective>
{
    /// <summary>
    /// The scheme that is allowed; always includes a colon (<c>:</c>).
    /// </summary>
    public string Scheme { get; } = Scheme.EndsWith(':') ? Scheme : $"{Scheme}:";
}
