using Swallow.ContentSecurityPolicy.Abstractions.V2.Model;
using Swallow.ContentSecurityPolicy.Abstractions.V2.Model.SourceExpressions;

namespace Swallow.ContentSecurityPolicy.Abstractions.V2;

/// <summary>
/// Shortcuts to create specific <see cref="ISourceExpression{T}"/>s
/// </summary>
public static class Allow
{
    // TODO: This can probably be easily source-generated.

    /// <inheritdoc cref="Model.SourceExpressions.DenyAll"/>
    public static DenyAll Nothing { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.Hash"/>
    public static Hash Hash(Hash.Algorithm algorithm, string hashedValue) => new(algorithm, hashedValue);

    /// <inheritdoc cref="Model.SourceExpressions.Hash"/>
    public static Hash Hash(Hash.Algorithm algorithm, byte[] hash) => new(algorithm, hash);

    /// <inheritdoc cref="Model.SourceExpressions.HostSource"/>
    public static HostSource Host(string host) => new(host);

    /// <inheritdoc cref="Model.SourceExpressions.InlineSpeculationRules"/>
    public static InlineSpeculationRules InlineSpeculationRules { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.Nonce"/>
    public static Nonce Nonce { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.ReportSample"/>
    public static ReportSample ReportSample { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.SchemeSource"/>
    public static SchemeSource Scheme(string scheme) => new(scheme);

    /// <inheritdoc cref="Model.SourceExpressions.Self"/>
    public static Self Self { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.StrictDynamic"/>
    public static StrictDynamic StrictDynamic { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.TrustedTypesEval"/>
    public static TrustedTypesEval TrustedTypesEval { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.UnsafeEval"/>
    public static UnsafeEval UnsafeEval { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.UnsafeHashes"/>
    public static UnsafeHashes UnsafeHashes { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.UnsafeInline"/>
    public static UnsafeInline UnsafeInline { get; } = new();

    /// <inheritdoc cref="Model.SourceExpressions.WasmUnsafeEval"/>
    public static WasmUnsafeEval WasmUnsafeEval { get; } = new();
}
