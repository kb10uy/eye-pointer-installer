using nadena.dev.ndmf;
using KusakaFactory.EyePointerInstaller.Runtime;
using UnityEngine;

namespace KusakaFactory.EyePointerInstaller
{
    internal sealed class InstallerState
    {
        private EyePointerNdmfInstaller _installer = null;

        public EyePointerNdmfInstaller Installer => _installer;

        private InstallerState(BuildContext context)
        {
            _installer = context.AvatarRootObject.GetComponentInChildren<EyePointerNdmfInstaller>();
        }

        public static InstallerState Initializer(BuildContext context) => new InstallerState(context);

        public void Destroy()
        {
            Object.DestroyImmediate(_installer);
            _installer = null;
        }
    }
}
