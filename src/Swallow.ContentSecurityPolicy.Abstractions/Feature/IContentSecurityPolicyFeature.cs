namespace Swallow.ContentSecurityPolicy.Abstractions.Feature;

/// <summary>
/// Contains all details about the content security policy for the current response.
/// </summary>
public interface IContentSecurityPolicyFeature
{
    /// <summary>
    /// The nonce to apply to all <see cref="Model.SourceExpressions.Nonce"/>-expressions.
    /// </summary>
    public string Nonce { get; }

    /// <summary>
    /// The <see cref="ContentSecurityPolicyDefinition"/> for the current response.
    /// </summary>
    public ContentSecurityPolicyDefinition? Policy { get; }
}
