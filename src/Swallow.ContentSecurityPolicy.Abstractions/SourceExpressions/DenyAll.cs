namespace Swallow.ContentSecurityPolicy.Abstractions.Directives;

public sealed class DenyAll : SourceExpression,
    IAppliesTo<DefaultSourceDirective>,
    IAppliesTo<ChildSourceDirective>,
    IAppliesTo<ConnectSourceDirective>,
    IAppliesTo<FontSourceDirective>,
    IAppliesTo<FormActionDirective>,
    IAppliesTo<FrameAncestorsDirective>,
    IAppliesTo<FrameSourceDirective>,
    IAppliesTo<ImageSourceDirective>,
    IAppliesTo<ManifestSourceDirective>,
    IAppliesTo<MediaSourceDirective>,
    IAppliesTo<ObjectSourceDirective>,
    IAppliesTo<ScriptSourceDirective>,
    IAppliesTo<ScriptSourceElementDirective>,
    IAppliesTo<StyleSourceDirective>,
    IAppliesTo<StyleSourceElementDirective>,
    IAppliesTo<WorkerSourceDirective>
{
    public static readonly DenyAll Instance = new();

    public override string Value => "'none'";
}
