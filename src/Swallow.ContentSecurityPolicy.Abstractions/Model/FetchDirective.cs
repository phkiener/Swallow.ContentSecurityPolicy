using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Swallow.ContentSecurityPolicy.Abstractions.Model;

/// <summary>
/// A specific kind of directive that controls which resources may be <em>fetched</em> (or executed).
/// </summary>
public interface IFetchDirective
{
    /// <summary>
    /// Enumerate the configured <see cref="ISourceExpression"/>s.
    /// </summary>
    public IEnumerable<ISourceExpression> Expressions { get; }
}

/// <summary>
/// A specific kind of directive that controls which resources may be <em>fetched</em> (or executed).
/// </summary>
/// <typeparam name="T">The deriving type; this is the "curiously recurring template pattern" (CRTP).</typeparam>
public abstract class FetchDirective<T> : Directive, IFetchDirective, IEnumerable<ISourceExpression<T>>
    where T : FetchDirective<T>
{
    private readonly List<ISourceExpression<T>> sourceExpressions = [];

    /// <summary>
    /// Enumerate the configured <see cref="ISourceExpression{T}"/>s.
    /// </summary>
    public IEnumerable<ISourceExpression<T>> Expressions => sourceExpressions.AsEnumerable();

    /// <inheritdoc />
    IEnumerable<ISourceExpression> IFetchDirective.Expressions => sourceExpressions.AsEnumerable();

    /// <summary>
    /// Add an expression to this directive.
    /// </summary>
    /// <param name="expression">The <see cref="ISourceExpression{T}"/> to add.</param>
    public void Add(ISourceExpression<T> expression)
    {
        sourceExpressions.Add(expression);
    }

    /// <summary>
    /// Add multiple expressions to this directive.
    /// </summary>
    /// <param name="source">The <see cref="ISourceExpression{T}"/>s to add.</param>
    public void AddRange(params IEnumerable<ISourceExpression<T>> source)
    {
        foreach (var sourceExpression in source)
        {
            Add(sourceExpression);
        }
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    IEnumerator<ISourceExpression<T>> IEnumerable<ISourceExpression<T>>.GetEnumerator() => sourceExpressions.GetEnumerator();

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    IEnumerator IEnumerable.GetEnumerator() => sourceExpressions.GetEnumerator();
}
