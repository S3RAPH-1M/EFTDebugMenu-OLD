using SPTarkov.TestModule.Utils;
using UnityEngine;

namespace SPTarkov.TestModule.Classes
{
    public static class FreeCam
    {
        public static bool Enabled = false;
        private static Vector3 Position = Vector3.zero;
        private static FreeCamera cam;
        public static Color Line = Color.yellow;

        public static void DoFreeCam()
        {
            cam = Camera.main.gameObject.GetOrAddComponent<FreeCamera>();
            if (cam == null)
            {
                return;
            }

            cam.enableInputCapture = Enabled;
            Globals.localPlayer.PointOfView = Enabled ? EFT.EPointOfView.FreeCamera : EFT.EPointOfView.FirstPerson;

            if (!Enabled)
            {
                if (Position != Vector3.zero)
                {
                    Position = Vector3.zero; // Reset the place to lock them, so next time we activate them we can set it
                }

                if (cam != null)
                {
                    GameObject.Destroy(cam);
                }

                MUtils.MRemoveLine(Globals.localPlayer.gameObject);
                return;
            }
            if (Position != Vector3.zero)
            {
                Globals.localPlayer.Teleport(Position, true); // Lock their player to the place where they activated it
            }
            else
            {
                Position = Globals.localPlayer.Transform.position; // Set the place to lock them
            }

            MUtils.MDrawLine(Globals.localPlayer.gameObject, MUtils.LoweredPos(), Line);
        }
    }
}
