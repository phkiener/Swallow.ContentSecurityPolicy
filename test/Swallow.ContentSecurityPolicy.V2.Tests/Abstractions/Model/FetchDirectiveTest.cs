using Swallow.ContentSecurityPolicy.Abstractions.V2.Model;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.Directives;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.V2.Tests.Abstractions.Model;

public sealed class FetchDirectiveTest
{
    [Test]
    public async Task ExpressionsApplyToDirectives()
    {
        // -- Group 1: All the expressions.
        var allTypes = new List<Type>
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

        await AssertSourceExpressions<DefaultSourceDirective>(allTypes);
        await AssertSourceExpressions<ScriptSourceDirective>(allTypes);
        await AssertSourceExpressions<StyleSourceDirective>(allTypes);
        await AssertSourceExpressions<ScriptSourceElementDirective>(allTypes.Except([typeof(UnsafeHashes)]));
        await AssertSourceExpressions<StyleSourceElementDirective>(allTypes.Except([typeof(UnsafeHashes)]));

        // -- Group 2: Only URL-based expressions
        var usualTypes = new List<Type>
        {
            typeof(DenyAll),
            typeof(HostSource),
            typeof(SchemeSource),
            typeof(Self)
        };

        await AssertSourceExpressions<BaseUriDirective>(usualTypes);
        await AssertSourceExpressions<ChildSourceDirective>(usualTypes);
        await AssertSourceExpressions<ConnectSourceDirective>(usualTypes);
        await AssertSourceExpressions<FontSourceDirective>(usualTypes);
        await AssertSourceExpressions<FormActionDirective>(usualTypes);
        await AssertSourceExpressions<FrameAncestorsDirective>(usualTypes);
        await AssertSourceExpressions<FrameSourceDirective>(usualTypes);
        await AssertSourceExpressions<ImageSourceDirective>(usualTypes);
        await AssertSourceExpressions<ManifestSourceDirective>(usualTypes);
        await AssertSourceExpressions<MediaSourceDirective>(usualTypes);
        await AssertSourceExpressions<ObjectSourceDirective>(usualTypes);
        await AssertSourceExpressions<WorkerSourceDirective>(usualTypes);

        // -- Group 3: Only inline-based expressions
        var attributeTypes = new List<Type>
        {
            typeof(DenyAll),
            typeof(UnsafeHashes),
            typeof(UnsafeInline),
            typeof(ReportSample)
        };

        await AssertSourceExpressions<ScriptSourceAttributeDirective>(attributeTypes);
        await AssertSourceExpressions<StyleSourceAttributeDirective>(attributeTypes);
    }

    private static async Task AssertSourceExpressions<T>(params IEnumerable<Type> expectedTypes) where T : FetchDirective<T>
    {
        var availableTypes = typeof(T).Assembly.GetTypes()
            .Where(static t => t.IsAssignableTo(typeof(ISourceExpression<T>)))
            .ToList();

        await Assert.That(availableTypes).IsEquivalentTo(expectedTypes).Because(typeof(T).Name);
    }
}
