namespace Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

public interface IAppliesTo<T> : ISourceExpression where T : Directive;
