namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

/// <summary>
/// Marker interface to specify that an <see cref="ISourceExpression"/> applies to a certain <see cref="Directive"/>.
/// </summary>
/// <typeparam name="T">The <see cref="Directive"/> this expression applies to.</typeparam>
public interface IAppliesTo<T> : ISourceExpression where T : Directive;

/// <summary>
/// A convenience type that allows you to add supported <see cref="ISourceExpression"/> to this directive.
/// </summary>
/// <remarks>
/// To be considered <em>supported</em>, the expression must implement <see cref="IAppliesTo{T}"/>.
/// <typeparamref name="T"/> is meant to be the type itself (also known as the "curiously recurring template pattern" CRTP):
/// <code>
/// <![CDATA[
/// public sealed class SpecificDirective
///     : FetchDirective<SpecificDirective>("...");
/// ]]>
/// </code>
/// </remarks>
/// <param name="name">Name of the directive.</param>
/// <typeparam name="T">The type that is expected to be found in <see cref="IAppliesTo{T}"/>.</typeparam>
public abstract class FetchDirective<T>(string name) : FetchDirective(name) where T : FetchDirective<T>
{
    /// <summary>
    /// Add an expression to this directive.
    /// </summary>
    /// <param name="sourceExpression">The expression to add.</param>
    public void Add(IAppliesTo<T> sourceExpression)
    {
        base.Add(sourceExpression);
    }
}
