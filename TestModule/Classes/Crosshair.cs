using SPTarkov.TestModule.Utils;
using UnityEngine;

namespace SPTarkov.TestModule.Classes
{
    public static class Crosshair
    {
        public static bool Enabled = false;
        public static float CrosshairSize = 5f;
        public static float CrosshairThickness = 1f;
        public static float CrosshairGap = 1f;
        public static Color color = Color.green;
        // Turns out it worked fine I'm just autistic :)

        public static void Draw()
        {
            if (!Enabled)
            {
                return;
            }
            if (Globals.firearmController == null)
            {
                return;
            }
            Ray r = new Ray(Globals.firearmController.Fireport.position, Globals.firearmController.WeaponDirection * 1f);
            if (!Physics.Raycast(r, out RaycastHit Hit, float.MaxValue))
            {
                return;
            }
            Vector3 HitPoint = Camera.main.WorldToScreenPoint(Hit.point);
            if (HitPoint.z <= 0.01f)
            {
                return;
            }

            Vector2 PointA = new Vector2(Mathf.Round(HitPoint.x - CrosshairGap), Mathf.Round(Screen.height - HitPoint.y));
            Vector2 PointB = new Vector2(Mathf.Round(HitPoint.x - CrosshairGap - CrosshairSize), Mathf.Round(Screen.height - HitPoint.y));
            MUtils.DrawLine(PointA, PointB, color, CrosshairThickness);

            PointA = new Vector2(Mathf.Round(HitPoint.x + CrosshairGap), Mathf.Round(Screen.height - HitPoint.y));
            PointB = new Vector2(Mathf.Round(HitPoint.x + CrosshairGap + CrosshairSize), Mathf.Round(Screen.height - HitPoint.y));
            MUtils.DrawLine(PointA, PointB, color, CrosshairThickness);

            PointA = new Vector2(Mathf.Round(HitPoint.x), Mathf.Round(Screen.height - HitPoint.y - CrosshairGap));
            PointB = new Vector2(Mathf.Round(HitPoint.x), Mathf.Round(Screen.height - HitPoint.y - CrosshairGap - CrosshairSize));
            MUtils.DrawLine(PointA, PointB, color, CrosshairThickness);

            PointA = new Vector2(Mathf.Round(HitPoint.x), Mathf.Round(Screen.height - HitPoint.y + CrosshairGap));
            PointB = new Vector2(Mathf.Round(HitPoint.x), Mathf.Round(Screen.height - HitPoint.y + CrosshairGap + CrosshairSize));
            MUtils.DrawLine(PointA, PointB, color, CrosshairThickness);
        }
    }
}
