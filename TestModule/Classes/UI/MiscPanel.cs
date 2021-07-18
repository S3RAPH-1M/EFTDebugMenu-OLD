using SPTarkov.TestModule.Utils;
using UnityEngine;

namespace SPTarkov.TestModule.Classes.UI
{
    public static class MiscPanel
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
            #region Noclip
            GUI.Label(NextLeft(), $"Noclip [{MUtils.NoDecimal(Globals.NoclipSpeed)}]");
            Noclip.Noclipping = GUI.Toggle(NextLeft(), Noclip.Noclipping, "Enabled");
            if (Noclip.Noclipping)
            {
                float __buffer = Globals.NoclipSpeed;
                Globals.NoclipSpeed = GUI.HorizontalSlider(NextLeft(), __buffer, 0f, 50f);
            }
            #endregion
            SeparatorL();
            #region Teleportation
            GUI.Label(NextRight(), "Teleportation");
            Teleport.TPEnabled = GUI.Toggle(NextRight(), Teleport.TPEnabled, "Enabled");
            if (Teleport.TPEnabled)
            {
                Teleport.VisualizeTP = GUI.Toggle(NextRight(), Teleport.VisualizeTP, "Visualized");
            }
            #endregion
            SeparatorR();
            #region God Mode
            GUI.Label(NextLeft(), "God Mode");
            Globals.IsInGodMode = GUI.Toggle(NextLeft(), Globals.IsInGodMode, "Enabled");
            if (GUI.Button(NextLeft(), "Update"))
            {
                GodMode.UpdateGod();
            }
            #endregion
            SeparatorL();
            #region Full Heal
            if (GUI.Button(NextRight(), "Full Heal"))
            {
                if (Globals.localPlayer == null)
                {
                    return;
                }

                if (Globals.localPlayer.ActiveHealthController == null)
                {
                    return;
                }

                if (!Globals.IsInGameplayMap)
                {
                    return;
                }

                for (int i = 0; i <= 7; i++)
                {
                    Globals.localPlayer.Heal((EBodyPart)i, 200f);
                    if (Globals.consoleScreen != null)
                    {
                        Globals.consoleScreen.AddLog($"Healed {(EBodyPart)i}", MUtils.GetColor("green"));
                    }
                }
                Globals.localPlayer.ActiveHealthController.ChangeHydration(100f);
                Globals.localPlayer.ActiveHealthController.ChangeEnergy(100f);
            }
            #endregion
            SeparatorR();
            #region No Recoil
            GUI.Label(NextLeft(), "No Recoil");
            NoRecoil.Enabled = GUI.Toggle(NextLeft(), NoRecoil.Enabled, "Enabled");
            #endregion
            SeparatorL();
            #region Remove Object
            if (GUI.Button(NextRight(), "Remove Object"))
            {
                RemoveObject.Remove();
            }
            #endregion
            SeparatorR();
            #region Free Cam
            GUI.Label(NextLeft(), $"Free Cam []");
            FreeCam.Enabled = GUI.Toggle(NextLeft(), FreeCam.Enabled, "Enabled");
            #endregion
            SeparatorL();
            #region Weather
            GUI.Label(NextRight(), "Weather");

            float _buffer = SetWeather.GlobalFogOvercast;
            GUI.Label(NextRight(), $"Fog Overcast [{SetWeather.GlobalFogOvercast}]");
            SetWeather.GlobalFogOvercast = GUI.HorizontalSlider(NextRight(), _buffer, 0f, 24f);
            _buffer = SetWeather.LightningSummonBandWidth;
            GUI.Label(NextRight(), $"LightningSummonBandWidth [{SetWeather.LightningSummonBandWidth}]");
            SetWeather.LightningSummonBandWidth = GUI.HorizontalSlider(NextRight(), _buffer, 0f, 24f);
            SetWeather.RainControllerEnabled = GUI.Toggle(NextRight(), SetWeather.RainControllerEnabled, "Rain Controller");
            if (SetWeather.RainControllerEnabled)
            {
                _buffer = SetWeather.RainControllerSetIntensity;
                GUI.Label(NextRight(), $"Rain Intensity [{SetWeather.RainControllerSetIntensity}]");
                SetWeather.RainControllerSetIntensity = GUI.HorizontalSlider(NextRight(), _buffer, 0f, 24f);
            }
            _buffer = SetWeather.TOD_SkyCycleHour;
            GUI.Label(NextRight(), $"TOD_SkyCycleHour [{SetWeather.TOD_SkyCycleHour}]");
            SetWeather.TOD_SkyCycleHour = GUI.HorizontalSlider(NextRight(), _buffer, 0f, 24f);

            if (GUI.Button(NextRight(), "Set"))
            {
                SetWeather.DoSetWeather();
            }
            #endregion
            SeparatorR();
            #region Crosshair
            GUI.Label(NextLeft(), "Crosshair");
            Crosshair.Enabled = GUI.Toggle(NextLeft(), Crosshair.Enabled, "Enabled");
            if (Crosshair.Enabled)
            {
                GUI.Label(NextLeft(), $"Thickness [{MUtils.NoDecimal(Crosshair.CrosshairThickness)}]");
                float buffer = Crosshair.CrosshairThickness;
                Crosshair.CrosshairThickness = GUI.HorizontalSlider(NextLeft(), buffer, 1f, 50f);
                GUI.Label(NextLeft(), $"Size [{MUtils.NoDecimal(Crosshair.CrosshairSize)}]");
                buffer = Crosshair.CrosshairSize;
                Crosshair.CrosshairSize = GUI.HorizontalSlider(NextLeft(), buffer, 1f, 50f);

                GUI.Label(NextLeft(), $"Gap [{MUtils.NoDecimal(Crosshair.CrosshairGap)}]");
                buffer = Crosshair.CrosshairGap;
                Crosshair.CrosshairGap = GUI.HorizontalSlider(NextLeft(), buffer, 0f, 15f);
            }
            #endregion
            SeparatorL();
            #region Stat Change
            GUI.Label(NextRight(), "Stat Change");
            StatChange.Enabled = GUI.Toggle(NextRight(), StatChange.Enabled, "Enabled");
            if (StatChange.Enabled)
            {
                // This is where I'd add sliders and shit later if I wanted to make them changeable,
                // for now it's fine how it is though!
            }
            #endregion
            SeparatorR();
            #region One Punch Man
            GUI.Label(NextLeft(), "Insta-Kill Melee");
            MaxMelee.Enabled = GUI.Toggle(NextLeft(), MaxMelee.Enabled, "Enabled");
            if (MaxMelee.Enabled)
            {


            }
            #endregion
            SeparatorL();
            #region Max Ergo
            GUI.Label(NextLeft(), "Maximum Ergo");
            MaxErgo.Enabled = GUI.Toggle(NextLeft(), MaxErgo.Enabled, "Enabled");
            if (MaxErgo.Enabled)
            {


            }
            #endregion
            SeparatorL();

            #region Reset
            LeftCount = 0;
            RightCount = 0;
            #endregion
        }
    }
}
