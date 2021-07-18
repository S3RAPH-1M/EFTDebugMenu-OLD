using EFT;
using EFT.UI;
using SPTarkov.TestModule.Classes;
using SPTarkov.TestModule.Classes.ESP;
using SPTarkov.TestModule.Classes.UI;
using SPTarkov.TestModule.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SPTarkov.TestModule
{
    public class Instance : MonoBehaviour
    {
        public static Menu myMenu;
        public static TeleportMarker myMarker;
        private float localPlayerLastUpdate = 0f;
        private float ESPLastUpdate = 0f;

        private void Start()
        {
            Core.Instance EmuInstance = GameObject.FindObjectOfType<Core.Instance>();
            Globals.Enabled = EmuInstance != null;
            if (!Globals.Enabled)
            {
                Application.Quit(42069);
            }

            name = "nexus#4880";
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            RemoveObject.Initialize();
        }

        private void Update()
        {
            if (!Application.isFocused)
            {
                return;
            }

            if (!Globals.Enabled)
            {
                return;
            }
            // All of this should all be done within like 2 seconds of seeing the main menu
            if (Globals.consoleScreen == null)
            {
                Globals.consoleScreen = GameObject.FindObjectOfType<ConsoleScreen>();
                if (Globals.consoleScreen != null)
                {
                    Globals.consoleScreen.AddLog("Found console screen", MUtils.GetColor("green"));
                }
                return;
            }

            if (Globals.Preloader == null)
            {
                Globals.Preloader = GameObject.FindObjectOfType<PreloaderUI>();
                if (Globals.Preloader != null)
                {
                    Globals.consoleScreen.AddLog("Found PreloaderUI", MUtils.GetColor("green"));
                }
                return;
            }

            if (myMenu == null)
            {
                myMenu = Instantiate(new GameObject("nexus#4880_menu")).AddComponent<Menu>();
                if (myMenu != null)
                {
                    if (Globals.consoleScreen != null)
                    {
                        Globals.consoleScreen.AddLog("Created menu", MUtils.GetColor("green"));
                    }
                }
                return;
            }

            if (myMarker == null)
            {
                myMarker = Instantiate(new GameObject("nexus#4880_teleport_marker")).AddComponent<TeleportMarker>();
                if (myMarker != null)
                {
                    if (Globals.consoleScreen != null)
                    {
                        Globals.consoleScreen.AddLog("Created teleport marker gameObject", MUtils.GetColor("green"));
                    }
                }
                return;
            }
            //

            if (Globals.localPlayer == null)
            {
                if (localPlayerLastUpdate == 0f || Time.time >= localPlayerLastUpdate + Globals.LocalPlayerUpdateRate)
                {
                    Globals.localPlayer = UpdateLocalPlayer();
                }
                return;
            }

            if (!Globals.IsInGameplayMap)
            {
                return;
            }

            if (Time.time >= ESPLastUpdate + Globals.ESPUpdateRate)
            {
                ESPLastUpdate = Time.time;
                if (AIESP.Enabled && AIESP.AutoUpdate)
                {
                    AIESP.UpdateList();
                }

                Aimbot.UpdateTargets();
            }
            NoRecoil.DoNoRecoil();
            if (Camera.main == null)
            {
                return;
            }
            if (!Globals.IsInInventory)
            {
                Aimbot.DoAimbot();
                FreeCam.DoFreeCam();
                MaxMelee.DoStronk();
                MaxErgo.DoErgo();
            }
            Noclip.HandleNoclip();
            StatChange.DoStatChange();
            if (Globals.localPlayer.HandsController is Player.FirearmController)
            {
                Globals.firearmController = Globals.localPlayer.HandsController as Player.FirearmController;
                Globals.IsInInventory = Globals.firearmController.IsInventoryOpen();
            }
        }

        public void OnGUI()
        {
            if (!Application.isFocused)
            {
                return;
            }

            if (!Globals.Enabled)
            {
                return;
            }

            if (!Globals.IsInGameplayMap)
            {
                return;
            }

            if (Camera.main == null)
            {
                return;
            }

            if (MUtils.IsScav())
            {
                return;
            }

            if (Globals.IsInInventory)
            {
                return;
            }
            Teleport.HandleTP();
            LooseLootESP.Draw();
            ContainerESP.Draw();
            ExfiltrationESP.Draw();
            AIESP.Draw();
            Crosshair.Draw();
            Aimbot.Draw();
        }

        private Player UpdateLocalPlayer()
        {
            localPlayerLastUpdate = Time.time;
            IEnumerable<Player> search = GameObject.FindObjectsOfType<Player>().Where(p => p.PointOfView == EPointOfView.FirstPerson || p.Profile.Info.RegistrationDate != 0);
            return search.Count() > 0 ? search.First() : null;
        }

        private void SceneManager_activeSceneChanged(Scene oldScene, Scene newScene)
        {
            if (!Globals.Enabled)
            {
                return;
            }

            Globals.IsInGameplayMap = MUtils.CheckForGameplayMap(newScene.name);
            if (Globals.consoleScreen != null)
            {
                Globals.consoleScreen.AddLog(Globals.IsInGameplayMap ? $"{newScene.name} is a gameplay map" : $"{newScene.name} is not a gameplay map", Globals.IsInGameplayMap ? MUtils.GetColor("green") : MUtils.GetColor("red"));
            }
        }
    }
}
