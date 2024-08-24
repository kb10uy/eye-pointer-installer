using UnityEngine;
using VRC.SDKBase;

namespace KusakaFactory.EyePointerInstaller.Runtime
{
    [AddComponentMenu("KusakaFactory/Automatic EyePointer Installer")]
    public sealed class EyePointerNdmfInstaller : MonoBehaviour, IEditorOnly
    {
        [Tooltip("Unity 標準の Aim Constraint ではなく VRC Aim Constraint を使用する。EyePointer 1.2 以降で動作します")]
        public bool UseVRCConstraint = false;
    }
}
