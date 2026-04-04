using Swallow.ContentSecurityPolicy.Abstractions.V2.Model;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.V2.Tests.Abstractions.Model;

public sealed class FetchDirectiveTest
{
    [Test]
    public async Task DirectivesWithAllExpressions()
    {
        var expectedTypes = new List<Type>
        {
            typeof(DenyAll),
            typeof(Hash),
            typeof(HostSource),
            typeof(InlineSpeculationRules),
            typeof(Nonce),
            typeof(ReportSample),
            typeof(SchemeSource),
            typeof(Self),
            typeof(StrictDynamic),
            typeof(TrustedTypesEval),
            typeof(UnsafeEval),
            typeof(UnsafeHashes),
            typeof(UnsafeInline),
            typeof(WasmUnsafeEval)
        };

        await AssertSourceExpressions<DefaultSourceDirective>(expectedTypes);
        await AssertSourceExpressions<ScriptSourceDirective>(expectedTypes);
        await AssertSourceExpressions<StyleSourceDirective>(expectedTypes);
        await AssertSourceExpressions<ScriptSourceElementDirective>(expectedTypes.Except([typeof(UnsafeHashes)]));
        await AssertSourceExpressions<StyleSourceElementDirective>(expectedTypes.Except([typeof(UnsafeHashes)]));

    }

    [Test]
    public async Task DirectivesWithUrlExpressions()
    {
        var expectedTypes = new List<Type>
        {
            typeof(DenyAll),
            typeof(HostSource),
            typeof(SchemeSource),
            typeof(Self)
        };

        await AssertSourceExpressions<BaseUriDirective>(expectedTypes);
        await AssertSourceExpressions<ChildSourceDirective>(expectedTypes);
        await AssertSourceExpressions<ConnectSourceDirective>(expectedTypes);
        await AssertSourceExpressions<FontSourceDirective>(expectedTypes);
        await AssertSourceExpressions<FormActionDirective>(expectedTypes);
        await AssertSourceExpressions<FrameAncestorsDirective>(expectedTypes);
        await AssertSourceExpressions<FrameSourceDirective>(expectedTypes);
        await AssertSourceExpressions<ImageSourceDirective>(expectedTypes);
        await AssertSourceExpressions<ManifestSourceDirective>(expectedTypes);
        await AssertSourceExpressions<MediaSourceDirective>(expectedTypes);
        await AssertSourceExpressions<ObjectSourceDirective>(expectedTypes);
        await AssertSourceExpressions<WorkerSourceDirective>(expectedTypes);
    }

    [Test]
    public async Task DirectivesWithInlineExpressions()
    {
        var expectedTypes = new List<Type>
        {
            typeof(DenyAll),
            typeof(UnsafeHashes),
            typeof(UnsafeInline),
            typeof(ReportSample)
        };

        await AssertSourceExpressions<ScriptSourceAttributeDirective>(expectedTypes);
        await AssertSourceExpressions<StyleSourceAttributeDirective>(expectedTypes);
    }

    private static async Task AssertSourceExpressions<T>(params IEnumerable<Type> expectedTypes) where T : FetchDirective<T>
    {
        var availableTypes = typeof(T).Assembly.GetTypes()
            .Where(static t => t.IsAssignableTo(typeof(ISourceExpression<T>)))
            .ToList();

        await Assert.That(availableTypes).IsEquivalentTo(expectedTypes).Because(typeof(T).Name);
    }
}
