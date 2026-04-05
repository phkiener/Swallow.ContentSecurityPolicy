using Swallow.ContentSecurityPolicy.Abstractions.Endpoints;

namespace Swallow.ContentSecurityPolicy.Abstractions;

/// <summary>
/// Specifies that the class or method that this attribute is applied to should not include the
/// default content security policy.
/// </summary>
/// <remarks>
/// The attributes are not additive; only the last encountered attribute is considered.
/// </remarks>
/// <seealso cref="IgnoreContentSecurityPolicyAttribute"/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class ContentSecurityPolicyAttribute(string? name) : Attribute, IContentSecurityPolicyData
{
    /// <inheritdoc />
    public string? Name { get; } = name;
}
