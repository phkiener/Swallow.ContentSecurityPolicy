using Swallow.ContentSecurityPolicy.Abstractions;
using Swallow.ContentSecurityPolicy.Abstractions.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Tests.Framework;

public interface ITestCase<out T>
{
    public T Instance { get; }
    public string ExpectedValue { get; }

    public Type Type => typeof(T);
}

public sealed class TestCases
{
    private static readonly Dictionary<Type, object> testCaseByType = new()
    {
        [typeof(DenyAll)] = new TestCase<DenyAll>(DenyAll.Instance, "'none'"),
        [typeof(Hash)] = new TestCase<Hash>(new Hash(Hash.Algorithm.SHA256, "ZwWTRa9dUMTjdC2ayBbSIwiG6XwjBaGP8G4Uvbd6Kkw="), "'sha256-ZwWTRa9dUMTjdC2ayBbSIwiG6XwjBaGP8G4Uvbd6Kkw='"),
        [typeof(HostSource)] = new TestCase<HostSource>(new HostSource("https://localhost:80/"), "https://localhost:80/"),
        [typeof(InlineSpeculationRules)] = new TestCase<InlineSpeculationRules>(InlineSpeculationRules.Instance, "'inline-speculation-rules'"),
        [typeof(Nonce)] = new TestCase<Nonce>(Nonce.Instance, $"'nonce-{Assertion.TestNonce}'"),
        [typeof(ReportSample)] = new TestCase<ReportSample>(ReportSample.Instance, "'report-sample'"),
        [typeof(SchemeSource)] = new TestCase<SchemeSource>(new SchemeSource("http"), "http:"),
        [typeof(Self)] = new TestCase<Self>(Self.Instance, "'self'"),
        [typeof(StrictDynamic)] = new TestCase<StrictDynamic>(StrictDynamic.Instance, "'strict-dynamic'"),
        [typeof(TrustedTypesEval)] = new TestCase<TrustedTypesEval>(TrustedTypesEval.Instance, "'trusted-types-eval'"),
        [typeof(UnsafeEval)] = new TestCase<UnsafeEval>(UnsafeEval.Instance, "'unsafe-eval'"),
        [typeof(UnsafeHashes)] = new TestCase<UnsafeHashes>(UnsafeHashes.Instance, "'unsafe-hashes'"),
        [typeof(UnsafeInline)] = new TestCase<UnsafeInline>(UnsafeInline.Instance, "'unsafe-inline'"),
        [typeof(WasmUnsafeEval)] = new TestCase<WasmUnsafeEval>(WasmUnsafeEval.Instance, "'wasm-unsafe-eval'"),
    };

    public static ITestCase<T> For<T>() where T : ISourceExpression
    {
        return testCaseByType.GetValueOrDefault(typeof(T)) as TestCase<T>
               ?? throw new NotImplementedException($"Test case for {typeof(T).Name} is missing.");
    }

    private sealed record TestCase<T>(T Instance, string ExpectedValue) : ITestCase<T>;
}
