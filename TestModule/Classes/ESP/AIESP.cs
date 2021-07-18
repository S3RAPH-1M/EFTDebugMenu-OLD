using EFT;
using SPTarkov.TestModule.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SPTarkov.TestModule.Classes.ESP
{
    public static class AIESP
    {
        public static bool Enabled = false;
        public static bool AutoUpdate = false;
        public static bool DrawText = false;
        public static bool DrawLines = false;
        public static bool DeadESP = false;
        public static float DrawDistance = 10f;
        private static List<Player> PlayerList;

        public static Color SnaplineColor = Color.green;
        public static Color TextColor = Color.green;

        public static void Draw()
        {
            if (!Enabled)
            {
                return;
            }

            if (PlayerList == null || !(PlayerList.Count > 0))
            {
                return;
            }

            foreach (Player player in PlayerList)
            {
                string DrawName = string.Empty;
                if (!player.ActiveHealthController.IsAlive)
                {
                    if (!DeadESP)
                    {
                        continue;
                    }
                    else
                    {
                        DrawName = "[DEAD] ";
                    }
                }
                Vector3 Position3D = player.Transform.position;
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

                DrawName = string.Concat(DrawName, "[", player.Side.ToString(), "]",
                    " ", player.Profile.Info.Nickname, Console.Out.NewLine, MUtils.NoDecimal(distance), "m");
                Color PreviousColor = GUI.color;
                GUI.color = TextColor;
                if (DrawText)
                {
                    GUI.Label(new Rect(Position2D.x - MUtils.CalculateWidth(DrawName) / 4f, Screen.height - Position2D.y, MUtils.CalculateWidth(DrawName), 100f), DrawName);
                }

                if (DrawLines)
                {
                    MUtils.DrawLine(new Vector2(Screen.width / 2f, Screen.height), new Vector2(Position2D.x, Screen.height - Position2D.y), SnaplineColor);
                }

                GUI.color = PreviousColor;
            }
        }

        public static void UpdateList() => PlayerList = GameObject.FindObjectsOfType<Player>().Where(player => player != Globals.localPlayer).ToList<Player>();
    }
}