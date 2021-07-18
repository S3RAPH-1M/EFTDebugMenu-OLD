using EFT;
using EFT.InventoryLogic;
using SPTarkov.TestModule.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SPTarkov.TestModule.Classes
{
    public static class Aimbot
    {
        public static bool Enabled = false;
        public static bool VisualizeTarget = false;
        public static bool VisualizeFOV = false;
        public static bool DynamicBoneSelection = true;
        public static IEnumerable<Player> Targets;
        public static int AimIndex = 0;
        public static float MinDist = 5f;
        public static float MaxDist = 300f;
        public static float PreferredDistance = MaxDist;
        public static float FOV = 60f;
        public static Player CurrentTarget = null;
        private static float LastChecked = 0f;

        #region m
        private static int vis_mask = 1 << 12 | 1 << 16;
        private static RaycastHit raycastHit;
        #endregion

        public static void DoAimbot()
        {
            if (!Enabled)
            {
                return;
            }

            if (Targets == null)
            {
                return;
            }

            if (!(Targets.Count() > 0))
            {
                return;
            }

            if (Time.time >= LastChecked + 1f || CurrentTarget == null)
            {
                GetTarget();
            }

            if (CurrentTarget == null)
            {
                return;
            }

            Vector3 HeadPos3D = MUtils.GetBonePosition(CurrentTarget, GetBone(CurrentTarget));
            Vector3 HeadPos2D = Camera.main.WorldToScreenPoint(HeadPos3D);
            if (HeadPos2D.z < 0.01f) // They went offscreen so they are no longer our target
            {
                CurrentTarget = null;
            }

            if (Vector2.Distance(Globals.CenterScreen, new Vector2(HeadPos2D.x, HeadPos2D.y)) > FOV)
            {
                CurrentTarget = null;
            }

            if (!Input.GetKey(Globals.AimbotAimKey))
            {
                return;
            }

            if (CurrentTarget != null)
            {
                AimAtPosition(HeadPos3D);
            }
        }

        private static void GetTarget()
        {
            foreach (Player Target in Targets)
            {
                if (Target == null)
                {
                    continue;
                }

                if (!Target.ActiveHealthController.IsAlive)
                {
                    continue; // They are not alive
                }
                // We will be using their bone position instead of their base position because later I will make it
                Vector3 HeadPosition3D = MUtils.GetBonePosition(Target, GetBone(Target));
                float Distance3D = Vector3.Distance(Camera.main.transform.position, HeadPosition3D);
                if (Distance3D <= 5f)
                {
                    continue; // We are returning here instead of skipping because
                }
                // When they are too close our aim breaks and this is to prevent us highjacking
                // Their aim so they can actually fight back
                if (Distance3D > PreferredDistance)
                {
                    continue; // They are too far
                }

                Vector3 ScreenPosition = Camera.main.WorldToScreenPoint(HeadPosition3D);
                if (ScreenPosition.z < 0.01f)
                {
                    continue; // They are off screen
                }
                // They are out of our FOV
                float Distance2D = Vector2.Distance(new Vector2(ScreenPosition.x, ScreenPosition.y), Globals.CenterScreen);
                if (Distance2D > FOV)
                {
                    continue;
                }
                // If we have made it this far then they are a candidate for our best target
                if (CurrentTarget != null) // If we have a target
                {
                    if (CurrentTarget != Target) // If it's already them, go to the next person
                    {
                        if (isVisible(Target) && isVisible(CurrentTarget))
                        {
                            continue;
                        }

                        float _Distance3D = Vector3.Distance(Camera.main.transform.position, CurrentTarget.Transform.position);
                        float _Distance2D = Vector2.Distance(Globals.CenterScreen, Camera.main.WorldToScreenPoint(CurrentTarget.Transform.position));
                        if (Distance3D > _Distance3D)
                        {
                            continue; // If the player that we have is closer to center screen
                        }

                        if (Distance2D > _Distance2D)
                        {
                            continue; // Then skip this player
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                CurrentTarget = Target; // Otherwise set it to them, this will also happen if our CurrentTarget is null
            }
            LastChecked = Time.time;
        }

        // neck[132] | chest[36] | stomach[29] | head[133]
        public static int GetBone(Player p)
        {
            if (p != null && DynamicBoneSelection)
            {
                if (isBodyPartVisible(p, 133))
                {
                    return 133;
                }
                else if (isBodyPartVisible(p, 132))
                {
                    return 132;
                }
                else if (isBodyPartVisible(p, 36))
                {
                    return 36;
                }
                else
                {
                    return GetBone(null);
                }
            }
            else
            {
                switch (AimIndex)
                {
                    case 0:
                        return 133;
                    case 1:
                        return 132;
                    case 2:
                        return 36;
                    case 3:
                        return 29;
                    default:
                        return 133;
                }
            }
        }

        public static void Draw()
        {
            if (!Enabled)
            {
                return;
            }

            if (VisualizeFOV)
            {
                MUtils.DrawCircle(new Vector2(Globals.CenterScreen.x, Globals.CenterScreen.y), FOV, Color.green, 1f);
            }

            if (VisualizeTarget)
            {
                if (CurrentTarget == null)
                {
                    return;
                }

                Vector3 HeadPosition2D = Camera.main.WorldToScreenPoint(MUtils.GetBonePosition(CurrentTarget, GetBone(CurrentTarget)));
                if (HeadPosition2D.z < 0.01f)
                {
                    return;
                }

                MUtils.DrawBox(HeadPosition2D.x - 10f, (Screen.height - HeadPosition2D.y) - 10f, 20f, 20f, Color.green);
            }
        }

        public static void AimAtPosition(Vector3 position)
        {
            Vector3 b = GetShootPos();
            float distance = Vector3.Distance(Camera.main.transform.position, CurrentTarget.Transform.position);
            Weapon wep = null;
            try { wep = Globals.localPlayer.Weapon; }
            catch { }
            finally
            {
                if (wep != null)
                {
                    float travelTime = distance / wep.CurrentAmmoTemplate.InitialSpeed;
                    position.x += (CurrentTarget.Velocity.x * travelTime);
                    position.y += (CurrentTarget.Velocity.y * travelTime);
                    Vector3 eulerAngles = Quaternion.LookRotation((position - b).normalized).eulerAngles;
                    if (eulerAngles.x > 180f)
                    {
                        eulerAngles.x -= 360f;
                    }

                    Globals.localPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
                }
            }
            ;
        }

        #region m
        public static bool isVisible(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.Head.position,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }

        public static bool isBodyPartVisible(Player player, int bodypart)
        {
            Vector3 BodyPartToAim = MUtils.GetBonePosition(player, bodypart);
            return Physics.Linecast(
                Camera.main.transform.position,
                BodyPartToAim,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        #endregion

        public static Vector3 GetShootPos()
        {
            if (Globals.localPlayer == null)
            {
                return Vector3.zero;
            }
            if (Globals.firearmController == null)
            {
                return Vector3.zero;
            }
            return Globals.firearmController.Fireport.position + Camera.main.transform.forward * 1f;
        }

        public static void UpdateTargets()
        {
            if (!Enabled)
            {
                return;
            }
            Targets = GameObject.FindObjectsOfType<Player>().ToList<Player>().Where(p => p.ActiveHealthController.IsAlive && p != Globals.localPlayer && Vector3.Distance(p.Transform.position, Globals.localPlayer.Transform.position) <= PreferredDistance);
        }
    }
}