using HarmonyLib;
using System;
using System.Collections.Generic;
using UI.MainHUD;

namespace FreedomUnits
{
    [HarmonyPatch(typeof(LocalPlayerHUDView))]
    internal class LocalPlayerHUDViewPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("SetupVE")]
        static void SetupVE(HUDResourceVE ___temperature)
        {
            ___temperature.SetThresholds(new List<Tuple<int, string>>
            {
                new Tuple<int, string>((int)Configs.GetTemperature(-20), "normal"),
                new Tuple<int, string>((int)Configs.GetTemperature(-200), "medium"),
                new Tuple<int, string>((int)Configs.GetTemperature(-400), "low")
            });
        }
    }
}
