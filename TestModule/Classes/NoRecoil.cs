using UnityEngine;

namespace SPTarkov.TestModule.Classes
{
    public static class NoRecoil
    {
        public static bool Enabled = false;

        public static void DoNoRecoil()
        {
            if (!Enabled)
            {
                return;
            }

            Globals.localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0f;
            Globals.localPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthXy = new Vector2(0f, 0f);
            Globals.localPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthZ = new Vector2(0f, 0f);
            Globals.localPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled = false;
            Globals.localPlayer.ProceduralWeaponAnimation._shouldMoveWeaponCloser = false;
            Globals.localPlayer.ProceduralWeaponAnimation.MotionReact.SwayFactors.x = 0f;
            Globals.localPlayer.ProceduralWeaponAnimation.MotionReact.SwayFactors.y = 0f;
            Globals.localPlayer.ProceduralWeaponAnimation.MotionReact.SwayFactors.z = 0f;
            Globals.localPlayer.ProceduralWeaponAnimation.Breath.Intensity = 0f;
            Globals.localPlayer.ProceduralWeaponAnimation.Walk.Intensity = 0f;
            Globals.localPlayer.ProceduralWeaponAnimation.Shootingg.Stiffness = 0f;
            Globals.localPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 0f;
            Globals.localPlayer.ProceduralWeaponAnimation.ForceReact.Intensity = 0f;
        }
    }
}
