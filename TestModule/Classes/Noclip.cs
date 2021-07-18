using UnityEngine;

namespace SPTarkov.TestModule.Classes
{
    public static class Noclip
    {
        private static Vector3 NoclipPos;
        public static bool Noclipping = false;

        public static void HandleNoclip()
        {
            // Even if we are not noclipping we want to keep our variable up to date or
            // else when we start noclipping we will instantly teleport to <0, 0, 0>
            if (!Noclipping)
            {
                NoclipPos = Globals.localPlayer.Transform.position;
                return;
            }

            Globals.localPlayer.MovementContext.FreefallTime = 0f;
            if (Input.GetKey(Globals.NoclipFloatUp))
            {
                Globals.localPlayer.MovementContext.FreefallTime = -Globals.NoclipYSpeed;
                NoclipPos = Globals.localPlayer.Transform.position;
            }
            else if (Input.GetKey(Globals.NoclipFloatDown))
            {
                Globals.localPlayer.MovementContext.FreefallTime = Globals.NoclipYSpeed;
                NoclipPos = Globals.localPlayer.Transform.position;
            }
            else
            {
                Globals.localPlayer.Transform.position = NoclipPos;
            }

            if (!Input.GetKey(Globals.NoclipMoveForward))
            {
                return;
            }

            NoclipPos += Camera.main.transform.forward * Globals.NoclipSpeed * Time.deltaTime;
        }
    }
}
