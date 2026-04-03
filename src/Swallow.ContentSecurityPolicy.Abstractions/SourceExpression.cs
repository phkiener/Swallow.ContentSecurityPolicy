namespace Swallow.ContentSecurityPolicy.Abstractions;

public interface ISourceExpression
{
    string Value { get; }
}

public abstract class SourceExpression : ISourceExpression
{
    public abstract string Value { get; }

    public override bool Equals(object? obj) => obj is SourceExpression other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
