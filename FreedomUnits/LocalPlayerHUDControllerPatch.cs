using HarmonyLib;

namespace FreedomUnits
{
    [HarmonyPatch(typeof(LocalPlayerHUDController))]
    internal class LocalPlayerHUDControllerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("UpdateTemperature")]
        static void UpdateTemperaturePrefix(CharacterAtmosphericDataTracker ___atmosphereData, out float __state)
        {
            __state = ___atmosphereData.AtmosphereData.Temperature;
            ___atmosphereData.AtmosphereData.Temperature = Configs.GetTemperature(__state);
        }

        [HarmonyPostfix]
        [HarmonyPatch("UpdateTemperature")]
        static void UpdateTemperaturePostfix(CharacterAtmosphericDataTracker ___atmosphereData, float __state)
        {
            ___atmosphereData.AtmosphereData.Temperature = __state;
        }
    }
}
