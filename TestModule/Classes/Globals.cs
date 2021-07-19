using EFT;
using EFT.UI;
using UnityEngine;

namespace SPTarkov.TestModule.Classes
{
    public static class Globals
    {
        public static bool IsInGameplayMap = false;
        public static bool IsInGodMode = false;
        public static string MenuTitle = "nexus#4880 - Test Module";
        public static Player localPlayer;
        public static ConsoleScreen consoleScreen;
        public static Vector2 CenterScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
        public static Player.FirearmController firearmController;
        public static bool IsInInventory = false;
        public static PreloaderUI Preloader;

        // Variables to change
        public static KeyCode MenuToggleKey = KeyCode.Insert;
        public static KeyCode TeleportModifier = KeyCode.LeftShift;
        public static KeyCode TeleportButton = KeyCode.F;
        public static KeyCode NoclipFloatUp = KeyCode.Space;
        public static KeyCode NoclipFloatDown = KeyCode.C;
        public static KeyCode NoclipMoveForward = KeyCode.W;
        public static KeyCode AimbotAimKey = KeyCode.Mouse1;

        public static float NoclipSpeed = 1f; // Speed while holding NoclipMoveForward
        public static float NoclipYSpeed = 0.5f; // Speed when using NoclipFloatUp/Down
        public static float LocalPlayerUpdateRate = 3f;
        public static float MaxESPDistance = 1000f;
        public static float ESPMinUpdateRate = 1f;
        public static float ESPMaxUpdateRate = 10f;
        public static float ESPUpdateRate = 3f;
    }
}
