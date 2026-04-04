namespace Swallow.ContentSecurityPolicy.Abstractions.V2;

/// <summary>
/// Format a given <see cref="ContentSecurityPolicyDefinition"/> into a representation that is
/// valid for use as a HTTP response header.
/// </summary>
public interface IContentSecurityPolicyWriter
{
    /// <summary>
    /// Format the given <see cref="ContentSecurityPolicyDefinition"/> into a response header value.
    /// </summary>
    /// <param name="policy">The <see cref="ContentSecurityPolicyDefinition"/> to format.</param>
    /// <returns>The formatted value.</returns>
    string Format(ContentSecurityPolicyDefinition policy);
}
