namespace Swallow.ContentSecurityPolicy.Abstractions;

public abstract class Directive(string name)
{
    public string Name { get; } = name;
    public abstract IEnumerable<ISourceExpression> Expressions { get; }
}
