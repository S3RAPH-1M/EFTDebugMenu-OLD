namespace SPTarkov.TestModule.Classes
{
    public static class GodMode
    {
        // Yes, god would still technically "disable" if we simply uncheck it, but
        // it will still call the events which may cause lag
        public static void UpdateGod()
        {
            if (Globals.localPlayer == null)
            {
                return;
            }

            if (Globals.localPlayer.ActiveHealthController == null)
            {
                return;
            }

            if (Globals.IsInGodMode)
            {
                Globals.localPlayer.ActiveHealthController.EnergyChangedEvent += ActiveHealthController_EnergyChangedEvent;
                Globals.localPlayer.ActiveHealthController.HydrationChangedEvent += ActiveHealthController_HydrationChangedEvent;
                Globals.localPlayer.ActiveHealthController.ApplyDamageEvent += ActiveHealthController_ApplyDamageEvent;
                Globals.localPlayer.ActiveHealthController.HealthChangedEvent += ActiveHealthController_HealthChangedEvent;
            }
            else
            {
                Globals.localPlayer.ActiveHealthController.EnergyChangedEvent -= ActiveHealthController_EnergyChangedEvent;
                Globals.localPlayer.ActiveHealthController.HydrationChangedEvent -= ActiveHealthController_HydrationChangedEvent;
                Globals.localPlayer.ActiveHealthController.ApplyDamageEvent -= ActiveHealthController_ApplyDamageEvent;
                Globals.localPlayer.ActiveHealthController.HealthChangedEvent -= ActiveHealthController_HealthChangedEvent;
            }
        }

        private static void ActiveHealthController_HealthChangedEvent(EBodyPart arg1, float arg2, GStruct241 arg3) => Globals.localPlayer.Heal(arg1, 100f);

        private static void ActiveHealthController_ApplyDamageEvent(EBodyPart arg1, float arg2, GStruct241 arg3) => Globals.localPlayer.Heal(arg1, 100f);

        // Prevents dehydration
        private static void ActiveHealthController_HydrationChangedEvent(float obj) => Globals.localPlayer.ActiveHealthController.ChangeHydration(100f);

        // Prevents hunger
        private static void ActiveHealthController_EnergyChangedEvent(float obj) => Globals.localPlayer.ActiveHealthController.ChangeEnergy(100f);
    }
}
