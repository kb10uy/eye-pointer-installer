using UnityEngine;
using UnityEngine.Animations;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Dynamics.Constraint.Components;
using nadena.dev.ndmf;
using KusakaFactory.EyePointerInstaller.Runtime;

[assembly: ExportsPlugin(typeof(KusakaFactory.EyePointerInstaller.EyePointerInstallerNdmfPlugin))]
namespace KusakaFactory.EyePointerInstaller
{
    internal sealed class EyePointerInstallerNdmfPlugin : Plugin<EyePointerInstallerNdmfPlugin>
    {
        public override string DisplayName => "EyePointer Installer";
        public override string QualifiedName => "org.kb10uy.eye-pointer-installer";

        protected override void Configure()
        {
            InPhase(BuildPhase.Transforming)
                .AfterPlugin("nadena.dev.modular-avatar")
                .Run("Insert dummy eye bones", FindAndInsallWithInstaller);
        }

        private void FindAndInsallWithInstaller(BuildContext ctx)
        {
            var installerComponent = ctx.AvatarRootObject.GetComponentInChildren<EyePointerNdmfInstaller>();
            if (installerComponent == null) return;

            ProcessInstall(ctx, installerComponent);
        }

        private void ProcessInstall(BuildContext ctx, EyePointerNdmfInstaller installer)
        {
            var (originalLeftEye, originalRightEye) = LocateEyeBones(ctx.AvatarRootObject);
            var dummyLeftEye = SubstituteEyeBone(originalLeftEye);
            var dummyRightEye = SubstituteEyeBone(originalRightEye);

            var target = LocateEyePointerTarget(installer);
            if (installer.UseVRCConstraint)
            {
                SetupConstaintForVRC(target.transform, dummyLeftEye);
                SetupConstaintForVRC(target.transform, dummyRightEye);
            }
            else
            {
                SetupConstaintForUnity(target.transform, dummyLeftEye);
                SetupConstaintForUnity(target.transform, dummyRightEye);
            }

            ReplaceAvatarDescriptorEyeBones(ctx.AvatarDescriptor, dummyLeftEye, dummyRightEye);

            Object.DestroyImmediate(installer);
        }

        private (GameObject LeftEye, GameObject RightEye) LocateEyeBones(GameObject avatarRoot)
        {
            // TODO: もっと intelligent にする
            var leftEyeTransform = avatarRoot.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/LeftEye");
            var rightEyeTransform = avatarRoot.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/RightEye");
            return (leftEyeTransform.gameObject, rightEyeTransform.gameObject);
        }

        private GameObject LocateEyePointerTarget(EyePointerNdmfInstaller installer)
        {
            var eyePointerTargetTransform = installer.transform.Find("Target");
            return eyePointerTargetTransform.gameObject;
        }

        private GameObject SubstituteEyeBone(GameObject originalEye)
        {
            // TODO: もっと intelligent にする
            var side = originalEye.name.Contains("L") ? "L" : "R";
            var dummyEye = new GameObject($"DummyEye_{side}");
            dummyEye.transform.SetParent(originalEye.transform.parent, true);
            dummyEye.transform.position = originalEye.transform.position;
            dummyEye.transform.localRotation = Quaternion.identity;
            originalEye.transform.SetParent(dummyEye.transform, true);
            return dummyEye;
        }

        private void SetupConstaintForVRC(Transform targetTransform, GameObject dummyEye)
        {
            var aimConstraint = dummyEye.AddComponent<VRCAimConstraint>();
            aimConstraint.enabled = false;
            aimConstraint.Sources.Add(new VRCConstraintSource(targetTransform, 1.0f, Vector3.zero, Vector3.zero));
            aimConstraint.AffectsRotationZ = false;
            aimConstraint.Locked = true;
            aimConstraint.IsActive = true;
        }

        private void SetupConstaintForUnity(Transform targetTransform, GameObject dummyEye)
        {
            var aimConstraint = dummyEye.AddComponent<AimConstraint>();
            aimConstraint.enabled = false;
            aimConstraint.AddSource(new ConstraintSource { sourceTransform = targetTransform, weight = 1.0f });
            aimConstraint.rotationAxis = Axis.X | Axis.Y;
            aimConstraint.locked = true;
            aimConstraint.constraintActive = true;
        }

        private void ReplaceAvatarDescriptorEyeBones(VRCAvatarDescriptor descriptor, GameObject leftEye, GameObject rightEye)
        {
            if (!descriptor.enableEyeLook) return;
            var settings = descriptor.customEyeLookSettings;
            settings.leftEye = leftEye.transform;
            settings.rightEye = rightEye.transform;
            // TODO: 角度の設定もいじるべき
            descriptor.customEyeLookSettings = settings;
        }
    }
}
