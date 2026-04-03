namespace Swallow.ContentSecurityPolicy.Abstractions;

/// <summary>
/// A single directive of a <see cref="ContentSecurityPolicy"/>.
/// </summary>
/// <param name="name">Name of the directive.</param>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy#directives">CSP Directives on MDN</seealso>
public abstract class Directive(string name)
{
    /// <summary>
    /// Name of the directive.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// The configured <see cref="ISourceExpression"/>s.
    /// </summary>
    public abstract IEnumerable<ISourceExpression> Expressions { get; }
}
