namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public interface IAppliesTo<T> : ISourceExpression where T : Directive;

public abstract class FetchDirective<T>(string name) : FetchDirective(name) where T : FetchDirective<T>
{
    public void Add(IAppliesTo<T> sourceExpression)
    {
        base.Add(sourceExpression);
    }
}
