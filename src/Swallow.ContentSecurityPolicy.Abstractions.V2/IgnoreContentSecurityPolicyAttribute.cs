using Swallow.ContentSecurityPolicy.Abstractions.V2.Metadata;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2;

/// <summary>
/// Specifies that the class or method that this attribute is applied to should not include the
/// default content security policy.
/// </summary>
/// <seealso cref="ContentSecurityPolicyAttribute"/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class IgnoreContentSecurityPolicyAttribute : Attribute, IIgnoreContentSecurityPolicy
{
    /// <inheritdoc />
    public override string ToString() => "Ignore Content Security Policy";
}
