namespace Swallow.ContentSecurityPolicy.Abstractions.V2.Metadata;

/// <summary>
/// Defines the <see cref="ContentSecurityPolicyDefinition"/> that should apply to this endpoint.
/// </summary>
/// <remarks>
/// If this metadata is not present, the default policy will be used instead.
/// </remarks>
/// <seealso cref="IIgnoreContentSecurityPolicy"/>
public interface IContentSecurityPolicyData
{
    /// <summary>
    /// Name of the policy to apply.
    /// </summary>
    string Name { get; }
}
