namespace Swallow.ContentSecurityPolicy.Abstractions.V2.Model;

/// <summary>
/// A source expression that applies to a specific kind of <see cref="Directive"/>.
/// </summary>
/// <typeparam name="T">The type of <see cref="Directive"/> to which this expression applies to.</typeparam>
public interface ISourceExpression<T> where T : FetchDirective<T>;
