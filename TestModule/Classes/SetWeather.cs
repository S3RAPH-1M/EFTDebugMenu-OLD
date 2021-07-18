using EFT.Weather;

namespace SPTarkov.TestModule.Classes
{
    public static class SetWeather
    {
        public static float RainControllerSetIntensity = 0f;
        public static float GlobalFogOvercast = 0f;
        public static float LightningSummonBandWidth = 0f;
        public static bool RainControllerEnabled = false;
        public static float TOD_SkyCycleHour = 12f;

        public static void DoSetWeather()
        {
            if (WeatherController.Instance != null)
            {
                WeatherController.Instance.RainController.SetIntensity(RainControllerSetIntensity);
                WeatherController.Instance.GlobalFogOvercast = GlobalFogOvercast;
                WeatherController.Instance.LightningSummonBandWidth = LightningSummonBandWidth;
                WeatherController.Instance.RainController.enabled = RainControllerEnabled;
            }
            if (TOD_Sky.Instance != null)
            {
                TOD_Sky.Instance.Components.Time.GameDateTime = null;
                TOD_Sky.Instance.Cycle.Hour = TOD_SkyCycleHour;
            }
        }
    }
}