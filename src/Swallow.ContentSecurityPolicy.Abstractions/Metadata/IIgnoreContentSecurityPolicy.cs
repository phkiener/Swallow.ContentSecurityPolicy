namespace Swallow.ContentSecurityPolicy.Abstractions.Metadata;

/// <summary>
/// Marker interface to skip including the default <see cref="ContentSecurityPolicyDefinition"/> if
/// it was configured.
/// </summary>
/// <remarks>
/// This takes precedence over <see cref="IContentSecurityPolicyData"/>.
/// </remarks>
public interface IIgnoreContentSecurityPolicy;
