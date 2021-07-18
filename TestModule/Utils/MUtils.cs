using EFT;
using SPTarkov.TestModule.Classes;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace SPTarkov.TestModule.Utils
{
    public static class MUtils
    {
        #region MyStuff
        public static IEnumerator Wait(float seconds, Action func)
        {
            yield return new WaitForSeconds(seconds);
            func.Invoke();
        }

        public static float CalculateWidth(string input) => 12 * input.Length;

        public static string GetColor(string color)
        {
            return string.Concat(new string[]
            {
                "<color=",
                color,
                ">",
                DateTime.Now.ToString("[HH:mm:ss]"),
                ": </color>"
            });
        }

        public static Vector3 LoweredPos() => new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y - 0.5f,
            Camera.main.transform.position.z
        );

        public static Vector3 GetBonePosition(Player p, int i) => p.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones.ElementAt(i).Value.position;

        // If you're reading this and curious I decided I didn't want decimals at all. :)
        public static string NoDecimal(float f) => string.Format("{0:0}", f);
        public static string DoubleDigit(float f) => string.Format(f % 1f == 0f ? "{0:0}" : "{0:0.00}", f);

        // TODO: Add the rest of the maps...
        public static bool CheckForGameplayMap(string sceneName)
        {
            switch (sceneName)
            {
                case "Factory_Day": // Factory
                    return true;
                case "Reserve_Base_DesignStuff": // Reserve
                    return true;
                case "custom_Light": // Customs
                    return true;
                case "woods_combined": // Woods
                    return true;
                case "Shopping_Mall_Terrain": // Interchange
                    return true;
                case "Laboratory_Scripts": // Labs
                    return true;
                case "develop": // Arena
                    return true;
                case "shoreline_scripts": // Shoreline
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsScav()
        {
            // Will return true because we don't want them to do whatever it is
            // whenever this returns true as we should always return on true
            if (Globals.localPlayer == null)
            {
                return true;
            }
            else
            {
                return Globals.localPlayer.Side == EPlayerSide.Savage;
            }
        }

        public static void MDrawLine(GameObject otherObject, Vector3 positionOne, Color col)
        {
            LineRenderer lr = otherObject.GetOrAddComponent<LineRenderer>();
            lr.enabled = false;
            lr.endColor = col;
            lr.startColor = col;
            lr.material.color = col;
            lr.endWidth = 0.02f;
            lr.startWidth = 0.02f;
            lr.SetPosition(0, positionOne);
            lr.SetPosition(1, otherObject.transform.position);
            lr.enabled = true;
        }

        public static void MRemoveLine(GameObject _object)
        {
            LineRenderer lr = _object.GetComponent<LineRenderer>();
            if (lr == null)
            {
                return;
            }

            lr.enabled = false;
        }
        #endregion

        #region StingraySrc
        public static Texture2D lineTex;
        #region DrawPixel
        public static void P(Vector2 Position, Color color, float thickness)
        {
            if (!lineTex) { lineTex = new Texture2D(1, 1); }
            float yOffset = Mathf.Ceil(thickness / 2f);
            Color savedColor = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(new Rect(Position.x, Position.y - (float)yOffset, thickness, thickness), lineTex);
            GUI.color = savedColor;
        }
        #endregion
        public static float Map(float value, float sourceFrom, float sourceTo, float destinationFrom, float destinationTo)
        {
            return ((value - sourceFrom) / (sourceTo - sourceFrom) * (destinationTo - destinationFrom) + destinationFrom);
        }
        public static void DrawBox(float x, float y, float w, float h, Color color)
        {
            DrawLine(new Vector2(x, y), new Vector2(x + w, y), color);
            DrawLine(new Vector2(x, y), new Vector2(x, y + h), color);
            DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color);
            DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color);
        }
        #region DrawLine - new with overloads
        public static void DrawLine(Rect rect) { DrawLine(rect, GUI.contentColor, 1.0f); }
        public static void DrawLine(Rect rect, Color color) { DrawLine(rect, color, 1.0f); }
        public static void DrawLine(Rect rect, float width) { DrawLine(rect, GUI.contentColor, width); }
        public static void DrawLine(Rect rect, Color color, float width) { DrawLine(new Vector2(rect.x, rect.y), new Vector2(rect.x + rect.width, rect.y + rect.height), color, width); }
        public static void DrawLine(Vector2 pointA, Vector2 pointB) { DrawLine(pointA, pointB, GUI.contentColor, 1.0f); }
        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color) { DrawLine(pointA, pointB, color, 1.0f); }
        public static void DrawLine(Vector2 pointA, Vector2 pointB, float width) { DrawLine(pointA, pointB, GUI.contentColor, width); }
        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
        {
            Matrix4x4 matrix = GUI.matrix;
            if (!lineTex) { lineTex = new Texture2D(1, 1); }
            Color savedColor = GUI.color;
            GUI.color = color;
            float angle = Vector3.Angle(pointB - pointA, Vector2.right);
            if (pointA.y > pointB.y) { angle = -angle; }
            GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
            GUIUtility.RotateAroundPivot(angle, pointA);
            GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1, 1), lineTex);
            GUI.matrix = matrix;
            GUI.color = savedColor;
        }
        #endregion
        public static void DrawCircle(Vector2 position, float r, Color color, float thickness)
        {
            const double PI = 3.1415926535;
            double i, angle, x1, y1;

            for (i = 0; i < 360; i += 0.1)
            {
                angle = i;
                x1 = r * Math.Cos(angle * PI / 180);
                y1 = r * Math.Sin(angle * PI / 180);
                P(new Vector2((float)(position.x + x1), (float)(position.y + y1)), color, thickness);
            }
        }
        #endregion
    }
}
