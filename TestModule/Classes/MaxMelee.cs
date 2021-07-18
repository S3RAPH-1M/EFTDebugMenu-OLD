namespace SPTarkov.TestModule.Classes
{
    public static class MaxMelee
    {
        public static bool Enabled = false;

        public static void DoStronk()
        {
            if (!Enabled)
            {
                return;
            }
            Globals.localPlayer.Skills.StrengthBuffMeleePowerInc.Value = 1337f;
        }
    }
}
