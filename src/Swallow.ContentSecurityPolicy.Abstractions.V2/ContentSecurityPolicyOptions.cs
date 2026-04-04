namespace Swallow.ContentSecurityPolicy.Abstractions.V2;

/// <summary>
/// Configuration for the inclusion of <see cref="ContentSecurityPolicyDefinition"/>s.
/// </summary>
public sealed class ContentSecurityPolicyOptions
{
    private readonly Dictionary<string, ContentSecurityPolicyDefinition> policies = [];

    /// <summary>
    /// The default policy that should be applied if no other policy is configured.
    /// </summary>
    public ContentSecurityPolicyDefinition? DefaultPolicy { get; set; }

    /// <summary>
    /// Resolve a policy that has been added using <see cref="AddPolicy(string, ContentSecurityPolicyDefinition)"/>.
    /// </summary>
    /// <param name="name">The name of the policy.</param>
    /// <returns>The found <see cref="ContentSecurityPolicyDefinition"/> or <see langword="null"/> if no such policy was added.</returns>
    public ContentSecurityPolicyDefinition? GetPolicy(string name)
    {
        return policies.GetValueOrDefault(name);
    }

    /// <summary>
    /// Set the <see cref="DefaultPolicy"/> to the given policy.
    /// </summary>
    /// <param name="defaultPolicy">The new <see cref="ContentSecurityPolicyDefinition"/> to set as default policy.</param>
    /// <seealso cref="SetDefaultPolicy(Action{ContentSecurityPolicyBuilder})"/>
    public ContentSecurityPolicyOptions SetDefaultPolicy(ContentSecurityPolicyDefinition? defaultPolicy)
    {
        DefaultPolicy = defaultPolicy;
        return this;
    }

    /// <summary>
    /// Set the <see cref="DefaultPolicy"/> to the policy configured by <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">Configuration of the <see cref="ContentSecurityPolicyBuilder"/>.</param>
    /// <seealso cref="SetDefaultPolicy(ContentSecurityPolicyDefinition)"/>
    public ContentSecurityPolicyOptions SetDefaultPolicy(Action<ContentSecurityPolicyBuilder> builder)
    {
        var policyBuilder = new ContentSecurityPolicyBuilder();
        builder(policyBuilder);

        return SetDefaultPolicy(policyBuilder.Build());
    }

    /// <summary>
    /// Set a named <see cref="ContentSecurityPolicyDefinition"/>.
    /// </summary>
    /// <remarks>
    /// If a policy with the same <paramref name="name"/> has already been added, it will be overwritten.
    /// </remarks>
    /// <param name="name">The name under which to store the policy.</param>
    /// <param name="policy">The new <see cref="ContentSecurityPolicyDefinition"/> to set as default policy.</param>
    /// <seealso cref="AddPolicy(string, Action{ContentSecurityPolicyBuilder})"/>
    public ContentSecurityPolicyOptions AddPolicy(string name, ContentSecurityPolicyDefinition policy)
    {
        policies[name] = policy;
        return this;
    }

    /// <summary>
    /// Set a named <see cref="ContentSecurityPolicyDefinition"/>.
    /// </summary>
    /// <remarks>
    /// If a policy with the same <paramref name="name"/> has already been added, it will be overwritten.
    /// </remarks>
    /// <param name="name">The name under which to store the policy.</param>
    /// <param name="builder">Configuration of the <see cref="ContentSecurityPolicyBuilder"/>.</param>
    /// <seealso cref="AddPolicy(string, ContentSecurityPolicyDefinition)"/>
    public ContentSecurityPolicyOptions AddPolicy(string name, Action<ContentSecurityPolicyBuilder> builder)
    {
        var policyBuilder = new ContentSecurityPolicyBuilder();
        builder(policyBuilder);

        return AddPolicy(name, policyBuilder.Build());
    }
}
