namespace Swallow.ContentSecurityPolicy.Abstractions.Endpoints;

/// <summary>
/// Defines that a <see cref="ContentSecurityPolicyDefinition"/> should apply to this endpoint.
/// </summary>
/// <remarks>
/// If this metadata is not present, the fallback policy will be used.
/// </remarks>
/// <seealso cref="IIgnoreContentSecurityPolicy"/>
public interface IContentSecurityPolicyData
{
    /// <summary>
    /// Name of the specific policy to apply, if any. If not set, the default policy will apply.
    /// </summary>
    string? Name { get; }
}
