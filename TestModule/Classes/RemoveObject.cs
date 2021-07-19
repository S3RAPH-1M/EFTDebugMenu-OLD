using EFT.UI;
using SPTarkov.TestModule.Utils;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SPTarkov.TestModule.Classes
{
    public static class RemoveObject
    {

        public static void Remove()
        {
            // Prevent usage of commands from main menu
            if (!Globals.IsInGameplayMap)
            {
                return;
            }
            // Prevent scav from using command
            if (MUtils.IsScav())
            {
                return;
            }

            Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (!Physics.Raycast(r, out RaycastHit Hit, 50f))
            {
                return;
            }

            if (Hit.transform == null)
            {
                return;
            }

            if (Hit.transform.gameObject == Globals.localPlayer.gameObject)
            {
                return;
            }

            Instance.Destroy(Hit.transform.gameObject);
        }

        private static void RemoveObjectCommand(Match obj) => Remove();
    }
}
