using System.Collections;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

/// <summary>
/// A <see cref="Directive"/> that is related to the fetching of resources.
/// </summary>
/// <param name="name">Name of the directive.</param>
public abstract class FetchDirective(string name) : Directive(name), IEnumerable<ISourceExpression>
{
    private readonly List<ISourceExpression> sourceExpressions = [];

    /// <inheritdoc />
    public sealed override IEnumerable<ISourceExpression> Expressions => EnumerateExpressions();

    /// <summary>
    /// Add an expression to this directive.
    /// </summary>
    /// <param name="sourceExpression">The <see cref="ISourceExpression"/> to add.</param>
    protected void Add(ISourceExpression sourceExpression)
    {
        sourceExpressions.Add(sourceExpression);
    }

    private IEnumerable<ISourceExpression> EnumerateExpressions()
    {
        if (!sourceExpressions.Any())
        {
            yield return DenyAll.Instance;
            yield break;
        }

        var hasStrictDynamic = sourceExpressions.OfType<StrictDynamic>().Any();
        foreach (var expression in sourceExpressions)
        {
            if (hasStrictDynamic && expression is HostSource or SchemeSource or Self or UnsafeInline)
            {
                continue;
            }

            yield return expression;
        }
    }

    IEnumerator<ISourceExpression> IEnumerable<ISourceExpression>.GetEnumerator()
    {
        return sourceExpressions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return sourceExpressions.GetEnumerator();
    }
}
