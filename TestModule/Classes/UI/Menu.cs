using UnityEngine;

namespace SPTarkov.TestModule.Classes.UI
{
    public class Menu : MonoBehaviour
    {
        private bool IsMenuOpen = false;
        private static int PanelCount = 0;
        private int ItemCount = 0;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                IsMenuOpen = !IsMenuOpen;
            }
        }

        public void OnGUI()
        {
            if (!IsMenuOpen)
            {
                return;
            }
            GUI.color = Color.white;
            GUI.backgroundColor = new Color(0f, 0f, 0f, 0f);
            GUI.Window(0, new Rect(0f, 200f, Screen.width, 755f), DrawUI, string.Empty);
            GUI.backgroundColor = Color.white;
            GUI.Box(new Rect(0f, 200f, 175f, 300f), Globals.MenuTitle);
            if (ESPPanel.Enabled)
            {
                GUI.Box(ESPPanel.MenuSize, "ESP");
            }

            if (MiscPanel.Enabled)
            {
                GUI.Box(MiscPanel.MenuSize, "Misc");
            }

            if (AimbotPanel.Enabled)
            {
                GUI.Box(AimbotPanel.MenuSize, "Aimbot");
            }

            if (ColorsPanel.Enabled)
            {
                GUI.Box(ColorsPanel.MenuSize, "Colors");
            }
        }

        private void DrawUI(int id)
        {
            ESPPanel.Enabled = GUI.Toggle(NextSpot(), ESPPanel.Enabled, "ESP");
            MiscPanel.Enabled = GUI.Toggle(NextSpot(), MiscPanel.Enabled, "Misc");
            AimbotPanel.Enabled = GUI.Toggle(NextSpot(), AimbotPanel.Enabled, "Aimbot");
            ColorsPanel.Enabled = GUI.Toggle(NextSpot(), ColorsPanel.Enabled, "Colors");

            ItemCount = 0;

            if (ESPPanel.Enabled)
            {
                ESPPanel.DrawUI();
            }

            if (MiscPanel.Enabled)
            {
                MiscPanel.DrawUI();
            }

            if (AimbotPanel.Enabled)
            {
                AimbotPanel.DrawUI();
            }

            if (ColorsPanel.Enabled)
            {
                ColorsPanel.DrawUI();
            }
        }

        private Rect NextSpot() => new Rect(5f, ItemCount++ * 20f + 40f, 55f, 15f);

        public static Rect NextPanel()
        {
            float y = 200f;
            float x = 185f + 420f * PanelCount;
            PanelCount++;
            return new Rect(x, y, 420f, 755f);
        }
    }
}