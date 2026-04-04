using Swallow.ContentSecurityPolicy.Abstractions.V2.Model;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2;

/// <summary>
/// A fluent builder to configure a <see cref="ContentSecurityPolicyDefinition"/>.
/// </summary>
public sealed partial class ContentSecurityPolicyBuilder
{
    private readonly List<Directive> directives = [];

    /// <summary>
    /// Add the given directive to the <see cref="ContentSecurityPolicyDefinition"/>.
    /// </summary>
    /// <remarks>
    /// If a directive of type <typeparamref name="T"/> already exists, it will be removed.
    /// </remarks>
    /// <param name="directive">The directive to add.</param>
    /// <typeparam name="T">Type of the directive to add.</typeparam>
    public ContentSecurityPolicyBuilder AddDirective<T>(T directive) where T : Directive
    {
        SetDirective(directive);
        return this;
    }

    /// <summary>
    /// Build the final <see cref="ContentSecurityPolicyDefinition"/>.
    /// </summary>
    /// <returns>The resulting <see cref="ContentSecurityPolicyDefinition"/>.</returns>
    public ContentSecurityPolicyDefinition Build()
    {
        return new ContentSecurityPolicyDefinition(directives);
    }

    private T? GetDirective<T>() where T : Directive
    {
        return directives.OfType<T>().FirstOrDefault();
    }

    private void SetDirective<T>(T? directive) where T : Directive
    {
        var existingDirective = GetDirective<T>();
        if (existingDirective is not null)
        {
            directives.Remove(existingDirective);
        }

        if (directive is not null)
        {
            directives.Add(directive);
        }
    }
}
