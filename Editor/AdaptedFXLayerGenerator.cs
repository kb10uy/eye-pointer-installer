using nadena.dev.ndmf;

namespace KusakaFactory.EyePointerInstaller
{
    internal sealed class AdaptedFXLayerGenerator : Pass<AdaptedFXLayerGenerator>
    {
        public override string QualifiedName => nameof(AdaptedFXLayerGenerator);
        public override string DisplayName => "Generate adapted FX Layer for avatar";

        protected override void Execute(BuildContext context)
        {
            var installer = context.GetState(InstallerState.Initializer).Installer;
            if (installer == null) return;
        }
    }
}
