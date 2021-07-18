using SPTarkov.TestModule.Utils;
using UnityEngine;

namespace SPTarkov.TestModule.Classes
{
    public static class Teleport
    {
        public static bool VisualizeTP = false;
        public static bool TPEnabled = false;

        public static void HandleTP()
        {
            if (VisualizeTP)
            {
                if (Instance.myMarker != null)
                {
                    Instance.myMarker.enabled = false;
                    MUtils.MRemoveLine(Instance.myMarker.gameObject);
                }
            }

            if (Globals.TeleportModifier != KeyCode.None)
            {
                if (!Input.GetKey(Globals.TeleportModifier))
                {
                    return;
                }
            }

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(r, out hit, 1000f))
            {
                return;
            }

            if (hit.transform == null)
            {
                return;
            }

            if (VisualizeTP)
            {
                if (Instance.myMarker != null)
                {
                    float distance = Vector3.Distance(MUtils.LoweredPos(), hit.point);
                    Instance.myMarker.gameObject.transform.position = hit.point;
                    Instance.myMarker.enabled = true;
                    Vector2 w2s = Camera.main.WorldToScreenPoint(hit.point);
                    GUI.color = Instance.myMarker.Line;
                    GUI.Label(new Rect(w2s.x - 50f, Screen.height - w2s.y, 100f, 50f), $"Teleport\n{MUtils.NoDecimal(distance)}m");
                    MUtils.MDrawLine(Instance.myMarker.gameObject, MUtils.LoweredPos(), Instance.myMarker.Line);
                }
            }

            if (!Input.GetKeyDown(Globals.TeleportButton))
            {
                return;
            }

            if (!TPEnabled)
            {
                return;
            }

            Globals.localPlayer.Teleport(hit.point, true);
        }
    }
}
