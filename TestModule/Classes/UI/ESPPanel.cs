using SPTarkov.TestModule.Classes.ESP;
using SPTarkov.TestModule.Utils;
using UnityEngine;

namespace SPTarkov.TestModule.Classes.UI
{
    public static class ESPPanel
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
            #region AI ESP
            GUI.Label(NextLeft(), $"AI: [{MUtils.NoDecimal(AIESP.DrawDistance)}m]");
            AIESP.Enabled = GUI.Toggle(NextLeft(), AIESP.Enabled, "Enabled");
            if (AIESP.Enabled)
            {
                AIESP.DrawLines = GUI.Toggle(NextLeft(), AIESP.DrawLines, "Lines");
                AIESP.DrawText = GUI.Toggle(NextLeft(), AIESP.DrawText, "Text");
                AIESP.AutoUpdate = GUI.Toggle(NextLeft(), AIESP.AutoUpdate, "Auto Update");
                AIESP.DeadESP = GUI.Toggle(NextLeft(), AIESP.DeadESP, "Dead");
                if (GUI.Button(NextLeft(), "Update"))
                {
                    AIESP.UpdateList();
                }

                float _buffer = AIESP.DrawDistance;
                AIESP.DrawDistance = GUI.HorizontalSlider(NextLeft(), _buffer, 1f, Globals.MaxESPDistance);
            }
            #endregion
            SeparatorL();
            #region Exfil ESP
            GUI.Label(NextRight(), $"Exfil: [{MUtils.NoDecimal(ExfiltrationESP.DrawDistance)}m]");
            ExfiltrationESP.Enabled = GUI.Toggle(NextRight(), ExfiltrationESP.Enabled, "Enabled");
            if (ExfiltrationESP.Enabled)
            {
                ExfiltrationESP.DrawLines = GUI.Toggle(NextRight(), ExfiltrationESP.DrawLines, "Lines");
                ExfiltrationESP.DrawText = GUI.Toggle(NextRight(), ExfiltrationESP.DrawText, "Text");
                if (GUI.Button(NextRight(), "Update"))
                {
                    ExfiltrationESP.UpdateList();
                }

                float _buffer = ExfiltrationESP.DrawDistance;
                ExfiltrationESP.DrawDistance = GUI.HorizontalSlider(NextRight(), _buffer, 1f, Globals.MaxESPDistance);
            }
            #endregion
            SeparatorR();
            #region Loose Loot ESP
            GUI.Label(NextLeft(), $"Loose Loot: [{MUtils.NoDecimal(LooseLootESP.DrawDistance)}m]");
            LooseLootESP.Enabled = GUI.Toggle(NextLeft(), LooseLootESP.Enabled, "Enabled");
            if (LooseLootESP.Enabled)
            {
                LooseLootESP.DrawLines = GUI.Toggle(NextLeft(), LooseLootESP.DrawLines, "Lines");
                LooseLootESP.DrawText = GUI.Toggle(NextLeft(), LooseLootESP.DrawText, "Text");
                if (GUI.Button(NextLeft(), "Update"))
                {
                    LooseLootESP.UpdateList();
                }

                float _buffer = LooseLootESP.DrawDistance;
                LooseLootESP.DrawDistance = GUI.HorizontalSlider(NextLeft(), _buffer, 1f, Globals.MaxESPDistance);
            }
            #endregion
            SeparatorL();
            #region Container ESP
            GUI.Label(NextRight(), $"Container: [{MUtils.NoDecimal(ContainerESP.DrawDistance)}m]");
            ContainerESP.Enabled = GUI.Toggle(NextRight(), ContainerESP.Enabled, "Enabled");
            if (ContainerESP.Enabled)
            {
                ContainerESP.DrawLines = GUI.Toggle(NextRight(), ContainerESP.DrawLines, "Lines");
                ContainerESP.DrawText = GUI.Toggle(NextRight(), ContainerESP.DrawText, "Text");
                if (GUI.Button(NextRight(), "Update"))
                {
                    ContainerESP.UpdateList();
                }

                float _buffer = ContainerESP.DrawDistance;
                ContainerESP.DrawDistance = GUI.HorizontalSlider(NextRight(), _buffer, 1f, Globals.MaxESPDistance);
            }
            #endregion
            SeparatorR();
            #region Update Rate
            float buffer = Globals.ESPUpdateRate;
            GUI.Label(new Rect(290f, 650f, 202.5f, 20f), $"Update Rate: {MUtils.NoDecimal(Globals.ESPUpdateRate)}s");
            Globals.ESPUpdateRate = GUI.HorizontalSlider(new Rect(290f, 670f, 202.5f, 20f), buffer, Globals.ESPMinUpdateRate, Globals.ESPMaxUpdateRate);
            #endregion
            SeparatorL();
            SeparatorR();

            #region Reset
            LeftCount = 0;
            RightCount = 0;
            #endregion
        }
    }
}