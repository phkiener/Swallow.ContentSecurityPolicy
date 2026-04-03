namespace Swallow.ContentSecurityPolicy.Abstractions;

/// <summary>
/// An expression for a <see cref="Directive"/>.
/// </summary>
public interface ISourceExpression
{
    /// <summary>
    /// The value of this expression.
    /// </summary>
    string Value { get; }
}

/// <summary>
/// A simple base-class for <see cref="ISourceExpression"/> that provides value equality.
/// </summary>
public abstract class SourceExpression : ISourceExpression
{
    /// <inheritdoc />
    public abstract string Value { get; }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is SourceExpression other && Value == other.Value;

    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode();
}
