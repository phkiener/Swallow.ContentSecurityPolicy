namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class DefaultSourceDirective() : FetchDirective(Name)
{
    public new const string Name = "default-src";

    public void Add(FetchSourceExpression sourceExpression)
    {
        AddSourceExpression(sourceExpression);
    }
}
