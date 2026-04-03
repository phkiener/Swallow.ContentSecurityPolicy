using Swallow.ContentSecurityPolicy.Abstractions.Directives;

namespace Swallow.ContentSecurityPolicy.Tests.Framework;

public abstract class FetchDirectiveTestBase<T> where T : FetchDirective<T>
{
    [Test]
    public async Task AcceptsConfiguredExpressions()
    {
        var testCases = EnumerateTestCases();
        await Assertion.ApplicableTypes(testCases);
    }

    [Test]
    public async Task WritesExpectedHeader()
    {
        var testCases = EnumerateTestCases();
        using (Assert.Multiple())
        {
            foreach (var testCase in testCases)
            {
                await Assertion.ContentSecurityPolicyAsync(p => Apply(p, testCase.Instance), $"{Name} {testCase.ExpectedValue}");
            }
        }
    }

    protected abstract string Name { get; }
    protected abstract void Apply(Abstractions.ContentSecurityPolicy policy, IAppliesTo<T> expression);
    protected abstract IEnumerable<ITestCase<IAppliesTo<T>>> EnumerateTestCases();
}
