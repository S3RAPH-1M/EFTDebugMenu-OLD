using SPTarkov.TestModule.Utils;
using UnityEngine;

namespace SPTarkov.TestModule.Classes.UI
{
    public static class AimbotPanel
    {
        public static bool Enabled = false;
        public static Rect MenuSize = Menu.NextPanel();

        #region Positioning
        private static int LeftCount = 0;
        private static int RightCount = 0;
        private static Rect NextLeft(float offset = 0f, float width = 202.5f, float height = 20f)
        {
            float x = MenuSize.x + 5f + offset;
            float y = 45f + LeftCount * 25f;
            LeftCount++;
            return new Rect(x, y, width, height);
        }

        private static Rect NextRight(float offset = 0f, float width = 202.5f, float height = 20f)
        {
            float x = MenuSize.x + 210f + offset;
            float y = 45f + RightCount * 25f;
            RightCount++;
            return new Rect(x, y, width, height);
        }

        // Leaves space between the groups
        private static void SeparatorL() => LeftCount++;
        private static void SeparatorR() => RightCount++;
        #endregion

        public static void DrawUI()
        {
            GUI.Label(NextLeft(), "Aimbot");
            Aimbot.Enabled = GUI.Toggle(NextLeft(), Aimbot.Enabled, "Enabled");
            if (Aimbot.Enabled)
            {
                Aimbot.VisualizeFOV = GUI.Toggle(NextLeft(), Aimbot.VisualizeFOV, "Visualize FOV");
                Aimbot.VisualizeTarget = GUI.Toggle(NextLeft(), Aimbot.VisualizeTarget, "Visualize Target");
                GUI.Label(NextLeft(), $"Minimum distance: {MUtils.NoDecimal(Aimbot.PreferredDistance)}m");
                float buffer = Aimbot.PreferredDistance;
                Aimbot.PreferredDistance = GUI.HorizontalSlider(NextLeft(), buffer, Aimbot.MinDist, Aimbot.MaxDist);
                GUI.Label(NextLeft(), $"FOV: {MUtils.NoDecimal(Aimbot.FOV)}m");
                buffer = Aimbot.FOV;
                Aimbot.FOV = GUI.HorizontalSlider(NextLeft(), buffer, 1f, 360f);
                Aimbot.DynamicBoneSelection = GUI.Toggle(NextLeft(), Aimbot.DynamicBoneSelection, "Dynamic Bone Selection");
                int _buffer = Aimbot.AimIndex;
                GUI.Label(NextLeft(), Aimbot.DynamicBoneSelection ? "Fallback Bone" : "Target Bone");
                Rect r = new Rect(NextLeft()) { height = 100f };
                Aimbot.AimIndex = GUI.SelectionGrid(r, _buffer, new string[] {
                    "head",
                    "neck",
                    "chest",
                    "stomach"
                }, 1);
            }
            #region Reset
            LeftCount = 0;
            RightCount = 0;
            #endregion
        }
    }
}