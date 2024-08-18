using UnityEngine;
using VRC.SDKBase;

namespace KusakaFactory.EyePointerInstaller.Runtime
{
    [AddComponentMenu("KusakaFactory/Automatic EyePointer Installer")]
    public sealed class EyePointerNdmfInstaller : MonoBehaviour, IEditorOnly
    {
        [Tooltip("Use VRC Aim Constraint instead of Unity's standard Aim Constraint. May causes incompatibility with specific versions of EyePointer.")]
        public bool UseVRCConstraint = false;
    }
}
