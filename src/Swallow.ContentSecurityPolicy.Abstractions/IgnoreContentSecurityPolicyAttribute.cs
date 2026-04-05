using Swallow.ContentSecurityPolicy.Abstractions.Metadata;

namespace Swallow.ContentSecurityPolicy.Abstractions;

/// <summary>
/// Specifies that the class or method that this attribute is applied to should not include the
/// default content security policy.
/// </summary>
/// <remarks>
/// This takes precedence over <see cref="ContentSecurityPolicyAttribute"/>.
/// </remarks>
/// <seealso cref="ContentSecurityPolicyAttribute"/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class IgnoreContentSecurityPolicyAttribute : Attribute, IIgnoreContentSecurityPolicy;
