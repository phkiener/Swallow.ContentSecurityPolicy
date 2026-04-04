using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Abstractions;

public interface IContentSecurityPolicyData
{
    string Name { get; }
}

public sealed class CspDefinition
{

}
public sealed class CspBuilder
{
    public CspBuilder BaseUri(IEnumerable<IAppliesTo<BaseUriDirective>> expressions)
    {
        return this;
    }

    public CspDefinition Build()
    {
        return new CspDefinition();
    }
}


/*
 * Options { default: Policy; policies: Dict<string, Policy>; }
 *   DefaultPolicy(...)
 *   AddPolicy(...) (Action<Builder> or Definition)
 *
 * Resolver(options)
 *   DefaultPolicy()
 *   Policy(name: string)
 *
 * Middleware
 *   - Find Endpoint, set relevant feature (Definition, Nonce)
 *     Register OnStarting -> Write CspDefinition
 *
 * ReportTo(...) -> local or URI
 *
 * ...
 *
 * Definition : IEnumerable<Directive> // Directive (Name)
 * Directive : ...
 * FetchDirective<T> : IEnumerable<ISourceExpressionFor<T>> -> ISourceExpressionFor<T> -> ISourceExpression
 *
 * ContentSecurityPolicyDefinition / ContentSecurityPolicyBuilder
 * NO ACRONYMS
 *
 * Setup:
 *      builder.Services.AddContentSecurityPolicy(
 *          opt => opt.AddDefaultPolicy(b => b.DefaultSource(Source.Self).ChildSource(Source.Self, Source.Scheme("http")))
 *              .AddPolicy("extra tight", b => b.DefaultSource(Source.None)))
 *          .AddReportHandler<DatadogLoggingHandler>();
 *
 *      or: builder.Services.AddContentSecurityPolicyViolationHandler<T>();
 *
 * app.UseContentSecurityPolicy();
 * app.MapContentSecurityPolicyViolations();
 *
 * IContentSecurityPolicyWriter
 * IDirectiveWriter() -> Directive to string, ISourceExpression to string
 * IReportHandler() - report
 *
 */
