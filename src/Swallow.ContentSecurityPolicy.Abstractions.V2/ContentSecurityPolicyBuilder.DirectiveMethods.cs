using Swallow.ContentSecurityPolicy.Abstractions.V2.Model;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2;

public partial class ContentSecurityPolicyBuilder
{
    private ContentSecurityPolicyBuilder AddSpecificDirective<T>(params IEnumerable<ISourceExpression<T>> expressions) where T : FetchDirective<T>, new()
    {
        var directive = new T();
        directive.AddRange(expressions);

        return AddDirective(directive);
    }

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="UpgradeInsecureRequestsDirective"/>.
    /// </summary>
    /// <param name="enabled">Whether to include the directive or remove it.</param>
    public ContentSecurityPolicyBuilder SetUpgradeInsecureRequests(bool enabled = true)
    {
        SetDirective(enabled ? new UpgradeInsecureRequestsDirective() : null);
        return this;
    }

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ReportToDirective"/>.
    /// </summary>
    /// <param name="uri">The URL to which to report the violations to.</param>
    public ContentSecurityPolicyBuilder SendReportsTo(string uri)
    {
        var directive = new ReportToDirective(uri);
        return AddDirective(directive);
    }

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ReportToDirective"/>.
    /// </summary>
    /// <param name="uri">The URL to which to report the violations to.</param>
    /// <param name="endpointName">The name to use for the endpoint.</param>
    public ContentSecurityPolicyBuilder SendReportsTo(string uri, string endpointName)
    {
        var directive = new ReportToDirective(uri, endpointName);
        return AddDirective(directive);
    }

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ReportToDirective"/> that
    /// directs reports to a locally hosted violation report handler.
    /// </summary>
    public ContentSecurityPolicyBuilder SendReportsToLocal()
    {
        var directive = new ReportToDirective(ReportToDirective.LocalEndpointUri);
        return AddDirective(directive);
    }

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ReportToDirective"/> that
    /// directs reports to a locally hosted violation report handler.
    /// </summary>
    /// <param name="endpointName">The name to use for the endpoint.</param>
    public ContentSecurityPolicyBuilder SendReportsToLocal(string endpointName)
    {
        var directive = new ReportToDirective(ReportToDirective.LocalEndpointUri, endpointName);
        return AddDirective(directive);
    }

    // TODO: This part should be source generated.

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="BaseUriDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="BaseUriDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddBaseUri(params IEnumerable<ISourceExpression<BaseUriDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ChildSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="ChildSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddChildSource(params IEnumerable<ISourceExpression<ChildSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ConnectSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="ConnectSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddConnectSource(params IEnumerable<ISourceExpression<ConnectSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="DefaultSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="DefaultSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddDefaultSource(params IEnumerable<ISourceExpression<DefaultSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="FontSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="FontSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddFontSource(params IEnumerable<ISourceExpression<FontSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="FormActionDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="FormActionDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddFormAction(params IEnumerable<ISourceExpression<FormActionDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="FrameAncestorsDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="FrameAncestorsDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddFrameAncestors(params IEnumerable<ISourceExpression<FrameAncestorsDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="FrameSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="FrameSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddFrameSource(params IEnumerable<ISourceExpression<FrameSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ImageSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="ImageSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddImageSource(params IEnumerable<ISourceExpression<ImageSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ManifestSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="ManifestSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddManifestSource(params IEnumerable<ISourceExpression<ManifestSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="MediaSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="MediaSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddMediaSource(params IEnumerable<ISourceExpression<MediaSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ObjectSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="ObjectSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddObjectSource(params IEnumerable<ISourceExpression<ObjectSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ScriptSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="ScriptSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddScriptSource(params IEnumerable<ISourceExpression<ScriptSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ScriptSourceAttributeDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="ScriptSourceAttributeDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddScriptSourceAttribute(params IEnumerable<ISourceExpression<ScriptSourceAttributeDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="ScriptSourceElementDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="ScriptSourceElementDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddScriptSourceElement(params IEnumerable<ISourceExpression<ScriptSourceElementDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="StyleSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="StyleSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddStyleSource(params IEnumerable<ISourceExpression<StyleSourceDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="StyleSourceAttributeDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="StyleSourceAttributeDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddStyleSourceAttribute(params IEnumerable<ISourceExpression<StyleSourceAttributeDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="StyleSourceElementDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="StyleSourceElementDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddStyleSourceElement(params IEnumerable<ISourceExpression<StyleSourceElementDirective>> expressions)
        => AddSpecificDirective(expressions);

    /// <summary>
    /// Configure the <see cref="ContentSecurityPolicyDefinition"/> to include a <see cref="WorkerSourceDirective"/> with
    /// the given expressions.
    /// </summary>
    /// <param name="expressions">The expressions for the <see cref="WorkerSourceDirective"/>.</param>
    public ContentSecurityPolicyBuilder AddWorkerSource(params IEnumerable<ISourceExpression<WorkerSourceDirective>> expressions)
        => AddSpecificDirective(expressions);
}
