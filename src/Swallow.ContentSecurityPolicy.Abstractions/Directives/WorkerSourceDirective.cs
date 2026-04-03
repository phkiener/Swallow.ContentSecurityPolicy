namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class WorkerSourceDirective() : FetchDirective<WorkerSourceDirective>(Name)
{
    public new const string Name = "worker-src";
}
