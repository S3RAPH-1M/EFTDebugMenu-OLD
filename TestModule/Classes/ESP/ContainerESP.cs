using EFT.Interactive;
using SPTarkov.TestModule.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SPTarkov.TestModule.Classes.ESP
{
    public static class ContainerESP
    {
        public static bool Enabled = false;
        public static bool DrawText = false;
        public static bool DrawLines = false;
        public static float DrawDistance = 10f;
        private static Color DrawColor = Color.grey;
        private static List<LootableContainer> LootableContainers;

        public static void Draw()
        {
            if (!Enabled)
            {
                return;
            }

            if (LootableContainers == null || !(LootableContainers.Count > 0))
            {
                return;
            }

            foreach (LootableContainer container in LootableContainers)
            {
                string DrawName = string.Empty;
                Vector3 Position3D = container.transform.position;
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

                DrawName = string.Concat(container.name.Replace("(Clone)", string.Empty).Replace("_", " "), Console.Out.NewLine, MUtils.NoDecimal(distance));
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

        public static void UpdateList() => LootableContainers = GameObject.FindObjectsOfType<LootableContainer>().ToList<LootableContainer>();
    }
}