using SPTarkov.TestModule.Classes.ESP;
using UnityEngine;

namespace SPTarkov.TestModule.Classes.UI
{
    public static class ColorsPanel
    {
        public static bool Enabled = false;
        public static Rect MenuSize = Menu.NextPanel();
        private static int SelectedPanel = -1;

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
            int buffer = SelectedPanel;
            SelectedPanel = GUI.SelectionGrid(NextLeft(0f, 202.5f, 100f), buffer, new string[]
            {
                "ESP",
                "MISC",
                "AIMBOT",
                "COLORS"
            }, 1);

            switch (SelectedPanel)
            {
                case 0:
                    Color previousColor = GUI.color;
                    #region AI ESP
                    GUI.color = AIESP.TextColor;
                    GUI.Label(NextRight(), "Text Color");
                    ColorChanger(ref AIESP.TextColor);
                    GUI.color = AIESP.SnaplineColor;
                    GUI.Label(NextRight(), "Snapline Color");
                    ColorChanger(ref AIESP.SnaplineColor);
                    #endregion
                    GUI.color = previousColor;
                    break;
                case 1:
                    previousColor = GUI.color;
                    #region Teleportation Marker
                    GUI.color = Instance.myMarker.Line;
                    GUI.Label(NextRight(), "Teleportation Marker");
                    ColorChanger(ref Instance.myMarker.Line);
                    #endregion
                    #region FreeCam
                    GUI.color = FreeCam.Line;
                    GUI.Label(NextRight(), "Free Cam");
                    ColorChanger(ref FreeCam.Line);
                    #endregion
                    #region Crosshair
                    GUI.color = Crosshair.color;
                    GUI.Label(NextRight(), "Crosshair");
                    ColorChanger(ref Crosshair.color);
                    #endregion
                    GUI.color = previousColor;
                    break;
                case 2:
                    previousColor = GUI.color;

                    GUI.color = previousColor;
                    break;
                case 3:
                    previousColor = GUI.color;

                    GUI.color = previousColor;
                    break;
            }

            #region Reset
            LeftCount = 0;
            RightCount = 0;
            #endregion
        }

        private static void ColorChanger(ref Color c)
        {
            float buffer = c.r;
            GUI.Label(NextRight(), "R");
            c.r = GUI.HorizontalSlider(NextRight(), buffer, 0f, 1f);
            buffer = c.g;
            GUI.Label(NextRight(), "G");
            c.g = GUI.HorizontalSlider(NextRight(), buffer, 0f, 1f);
            buffer = c.b;
            GUI.Label(NextRight(), "B");
            c.b = GUI.HorizontalSlider(NextRight(), buffer, 0f, 1f);
            // I don't know why you'd ever want to change the alpha, but here it is...
            buffer = c.a;
            GUI.Label(NextRight(), "A");
            c.a = GUI.HorizontalSlider(NextRight(), buffer, 0f, 1f);
        }
    }
}
