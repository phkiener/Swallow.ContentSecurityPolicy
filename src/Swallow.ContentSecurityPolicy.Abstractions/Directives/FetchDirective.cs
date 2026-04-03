using System.Collections;

namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public abstract class FetchDirective(string name) : Directive, IEnumerable<FetchSourceExpression>
{
    private readonly HashSet<FetchSourceExpression> sourceExpressions = [];

    public sealed override string Name { get; } = name;
    public sealed override IEnumerable<SourceExpression> Expressions => EnumerateExpressions();

    protected void AddSourceExpression(FetchSourceExpression sourceExpression)
    {
        sourceExpressions.Add(sourceExpression);
    }

    private IEnumerable<FetchSourceExpression> EnumerateExpressions()
    {
        if (sourceExpressions.Any())
        {
            return sourceExpressions.AsEnumerable();
        }

        return [DenyAll.Instance];
    }

    IEnumerator<FetchSourceExpression> IEnumerable<FetchSourceExpression>.GetEnumerator()
    {
        return sourceExpressions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return sourceExpressions.GetEnumerator();
    }
}
