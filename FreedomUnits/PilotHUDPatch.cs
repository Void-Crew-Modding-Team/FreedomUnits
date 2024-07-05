using Gameplay.SpacePlatforms;
using HarmonyLib;
using UI.PilotHUD;
using UnityEngine.UIElements;

namespace FreedomUnits
{
    [HarmonyPatch(typeof(PilotHUD))]
    internal class PilotHUDPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        static void Update(bool ___inVoid, Label ___speedIndicator, MovingSpacePlatform ___ship)
        {
            if (!___inVoid)
            {
                ___speedIndicator.text = Configs.GetSpeed(___ship.Velocity.magnitude).ToString("#") + " " + Configs.GetSpeedUnit();
            }
        }
    }
}
