using System.Collections;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public abstract class FetchDirective(string name) : Directive, IEnumerable<ISourceExpression>
{
    private readonly HashSet<ISourceExpression> sourceExpressions = [];

    public sealed override string Name { get; } = name;
    public sealed override IEnumerable<ISourceExpression> Expressions => EnumerateExpressions();

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

public abstract class FetchDirective<T>(string name) : FetchDirective(name) where T : FetchDirective<T>
{
    public void Add(IAppliesTo<T> sourceExpression)
    {
        base.Add(sourceExpression);
    }
}
