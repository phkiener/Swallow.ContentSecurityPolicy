using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.Reports;

namespace Swallow.ContentSecurityPolicy.Tests;

public sealed class ServiceProviderConfigTest
{
    [Test]
    public async Task ReportHandler_IsRegistered()
    {
        var serviceCollection = new ServiceCollection()
            .AddContentSecurityPolicyReportHandler<MyHandler>();

        await using var serviceProvider = serviceCollection.BuildServiceProvider();
        var service = serviceProvider.GetService<IReportHandler>();

        await Assert.That(service).IsTypeOf<MyHandler>();
        await Assert.That(serviceCollection.Single(t => t.ServiceType == typeof(IReportHandler)).Lifetime).IsEqualTo(ServiceLifetime.Scoped);
    }

    [Test]
    public async Task ExistingSupportedRegistrations_AreNotOverwritten()
    {
        var serviceCollection = new ServiceCollection()
            .AddSingleton<IReportHandler, MyHandler>()
            .AddSingleton<IContentSecurityPolicyHeaderWriter, MyHeaderWriter>()
            .AddSingleton<IContentSecurityPolicyResolver, MyResolver>()
            .AddSingleton<IContentSecurityPolicyNonceGenerator, MyNonceGenerator>()
            .AddContentSecurityPolicy();

        await using var serviceProvider = serviceCollection.BuildServiceProvider();
        await Assert.That(serviceProvider.GetRequiredService<IReportHandler>()).IsTypeOf<MyHandler>();
        await Assert.That(serviceProvider.GetRequiredService<IContentSecurityPolicyHeaderWriter>()).IsTypeOf<MyHeaderWriter>();
        await Assert.That(serviceProvider.GetRequiredService<IContentSecurityPolicyResolver>()).IsTypeOf<MyResolver>();
        await Assert.That(serviceProvider.GetRequiredService<IContentSecurityPolicyNonceGenerator>()).IsTypeOf<MyNonceGenerator>();
    }

    private sealed class MyHandler : IReportHandler
    {
        public Task Handle(ViolationReport[] violationReports, CancellationToken cancellationToken) => Task.CompletedTask;
    }

    private sealed class MyHeaderWriter : IContentSecurityPolicyHeaderWriter
    {
        public void WriteTo(IHeaderDictionary headers, ContentSecurityPolicyDefinition policy, ContentSecurityPolicyWriterContext context) { }
    }

    private sealed class MyResolver : IContentSecurityPolicyResolver
    {
        public ContentSecurityPolicyDefinition? DefaultPolicy() => null;

        public ContentSecurityPolicyDefinition? GetPolicy(string name) => null;
    }

    private sealed class MyNonceGenerator : IContentSecurityPolicyNonceGenerator
    {
        public string Generate(HttpContext httpContext) => "not really unique";
    }
}
