using nadena.dev.ndmf;

[assembly: ExportsPlugin(typeof(KusakaFactory.EyePointerInstaller.EyePointerInstallerNdmfPlugin))]
namespace KusakaFactory.EyePointerInstaller
{
    internal sealed class EyePointerInstallerNdmfPlugin : Plugin<EyePointerInstallerNdmfPlugin>
    {
        public override string DisplayName => "EyePointer Installer";
        public override string QualifiedName => "org.kb10uy.eye-pointer-installer";

        protected override void Configure()
        {
            InPhase(BuildPhase.Generating)
                .Run(new AdaptedFXLayerGenerator());

            InPhase(BuildPhase.Transforming)
                // cf. https://github.com/bdunderscore/modular-avatar/issues/1036
                .AfterPlugin("nadena.dev.modular-avatar")
                .Run(new EyeBonesModifier());
        }
    }
}
