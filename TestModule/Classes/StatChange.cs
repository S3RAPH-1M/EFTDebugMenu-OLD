namespace SPTarkov.TestModule.Classes
{
    public static class StatChange
    {
        public static bool Enabled = false;

        public static void DoStatChange()
        {
            if (!Enabled)
            {
                return;
            }
            // May add ability to actually change these values later idk I really don't care for it...
            Globals.localPlayer.Skills.StrengthBuffThrowDistanceInc.Value = 1f;
            Globals.localPlayer.Skills.StrengthBuffLiftWeightInc.Value = 1f;
            Globals.localPlayer.Skills.StrengthBuffSprintSpeedInc.Value = 1f;
            Globals.localPlayer.Skills.MagDrillsInstantCheck.Value = true;
            Globals.localPlayer.Skills.MagDrillsLoadSpeed.Value = 100f;
            Globals.localPlayer.Skills.MagDrillsUnloadSpeed.Value = 100f;
            Globals.localPlayer.Skills.StrengthBuffJumpHeightInc.Value = 0.45f;
            Globals.localPlayer.Skills.EnduranceBuffEnduranceInc.Value = 5f;
            Globals.localPlayer.Skills.HealthOfflineRegenerationInc.Value = 5f;
            Globals.localPlayer.Skills.HealthBreakChanceRed.Value = 1f;
            Globals.localPlayer.Skills.VitalityBuffSurviobilityInc.Value = 1f;
            Globals.localPlayer.Skills.VitalityBuffBleedChanceRed.Value = 1f;
            Globals.localPlayer.Skills.EnduranceBuffRestoration.Value = 5f;
            Globals.localPlayer.Skills.PerceptionHearing.Value = 1f;
            Globals.localPlayer.Skills.CovertMovementSoundVolume.Value = 1f;
            Globals.localPlayer.Skills.CovertMovementSpeed.Value = 1f;
            Globals.localPlayer.Skills.CovertMovementEquipment.Value = 1f;
            Globals.localPlayer.Skills.CovertMovementLoud.Value = 1f;
        }
    }
}
