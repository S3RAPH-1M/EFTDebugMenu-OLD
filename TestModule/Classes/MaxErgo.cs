namespace SPTarkov.TestModule.Classes
{
    public static class MaxErgo
    {
        public static bool Enabled = false;

        public static void DoErgo()
        {
            if (!Enabled)
            {
                return;
            }
            Globals.localPlayer.Weapon.Template.Ergonomics = 100f;
        }
    }
}
