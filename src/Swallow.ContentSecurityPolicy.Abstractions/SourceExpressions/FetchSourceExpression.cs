using Swallow.ContentSecurityPolicy.Abstractions.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Abstractions;

public abstract class FetchSourceExpression : SourceExpression, IAppliesTo<DefaultSourceDirective>;
