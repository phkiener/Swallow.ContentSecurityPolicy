using Microsoft.AspNetCore.Http;
using Swallow.ContentSecurityPolicy.Abstractions.V2;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;

namespace Swallow.ContentSecurityPolicy.V2;

/// <summary>
/// The context containing additional information for the <see cref="IContentSecurityPolicyHeaderWriter"/>.
/// </summary>
/// <param name="Nonce">The nonce to supply to all <see cref="Nonce"/> expressions.</param>
/// <param name="LocalReportingUri">The URL to use for locally-handled <see cref="ReportToDirective"/>s.</param>
public sealed record ContentSecurityPolicyWriterContext(string Nonce, string? LocalReportingUri);

/// <summary>
/// Format a given <see cref="ContentSecurityPolicyDefinition"/> into a representation that is
/// valid for use as a HTTP response header.
/// </summary>
public interface IContentSecurityPolicyHeaderWriter
{
    /// <summary>
    /// Write the given <see cref="ContentSecurityPolicyDefinition"/> to a <see cref="IHeaderDictionary"/>.
    /// </summary>
    /// <param name="headers">The <see cref="IHeaderDictionary"/> to write into.</param>
    /// <param name="policy">The <see cref="ContentSecurityPolicyDefinition"/> to format.</param>
    /// <param name="context">Additional information required to write the <see cref="ContentSecurityPolicyDefinition"/>.</param>
    void WriteTo(IHeaderDictionary headers, ContentSecurityPolicyDefinition policy, ContentSecurityPolicyWriterContext context);
}
