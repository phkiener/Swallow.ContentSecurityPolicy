namespace Swallow.ContentSecurityPolicy.Abstractions;

public abstract class Directive
{
    public abstract string Name { get; }
    public abstract IEnumerable<ISourceExpression> Expressions { get; }
}
