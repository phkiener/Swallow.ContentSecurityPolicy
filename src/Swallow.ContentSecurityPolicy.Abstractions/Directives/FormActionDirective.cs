namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class FormActionDirective() : FetchDirective<FormActionDirective>(Name)
{
    public new const string Name = "form-action";
}
