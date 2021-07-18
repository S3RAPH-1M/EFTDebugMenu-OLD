using EFT;
using EFT.Interactive;
using SPTarkov.TestModule.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SPTarkov.TestModule.Classes.ESP
{
    public static class LooseLootESP
    {
        public static bool Enabled = false;
        public static bool DrawText = false;
        public static bool DrawLines = false;
        public static float DrawDistance = 10f;
        private static Color DrawColor = Color.green;
        private static List<LootItem> LootItems;

        public static void Draw()
        {
            if (!Enabled)
            {
                return;
            }

            if (LootItems == null || !(LootItems.Count > 0))
            {
                return;
            }

            foreach (LootItem item in LootItems)
            {
                string DrawName = string.Empty;
                Vector3 Position3D = item.transform.position;
                float distance = Vector3.Distance(Position3D, Camera.main.transform.position);
                if (distance > DrawDistance)
                {
                    continue;
                }

                Vector3 Position2D = Camera.main.WorldToScreenPoint(Position3D);
                if (Position2D.z < 0.01f)
                {
                    continue;
                }

                if (item.Item == null)
                {
                    continue;
                }

                DrawName = string.Concat(item.name.Replace("(Clone)", string.Empty).Replace("_", " "), Console.Out.NewLine, MUtils.NoDecimal(distance), "m");
                Color PreviousColor = GUI.color;
                GUI.color = DrawColor;
                if (DrawText)
                {
                    GUI.Label(new Rect(Position2D.x, Screen.height - Position2D.y, 100f, 50f), DrawName);
                }

                if (DrawLines)
                {
                    MUtils.DrawLine(new Vector2(Screen.width / 2f, Screen.height), new Vector2(Position2D.x, Screen.height - Position2D.y), DrawColor);
                }

                GUI.color = PreviousColor;
            }
        }

        public static void UpdateList() => LootItems = GameObject.FindObjectsOfType<LootItem>().Where(item => item.GetComponent<Player>() == null).ToList<LootItem>();
    }
}