using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UI.MainHUD;

namespace FreedomUnits
{
    internal class Configs
    {
        private static readonly FieldInfo viewField = AccessTools.Field(typeof(PlayerHUD), "view");
        private static readonly FieldInfo temperatureField = AccessTools.Field(typeof(LocalPlayerHUDView), "temperature");

        internal enum TemperatureUnit
        {
            Celsius,
            Fahrenheit,
            Kelvin,
            Rankine,
            Delisle,
            Reaumur,
            Romer,
            Custom
        }

        internal static ConfigEntry<TemperatureUnit> TemperatureUnitConfig;
        internal static ConfigEntry<float> TemperatureMultiplier;
        internal static ConfigEntry<float> TemperatureAddend;

        internal static float GetTemperature(float celsius)
        {
            return TemperatureUnitConfig.Value switch
            {
                TemperatureUnit.Celsius => celsius,
                TemperatureUnit.Fahrenheit => celsius*1.8f + 32f,
                TemperatureUnit.Kelvin => celsius + 273.15f,
                TemperatureUnit.Rankine => (celsius + 273.15f)*1.8f,
                TemperatureUnit.Delisle => (100f - celsius)*1.5f,
                TemperatureUnit.Reaumur => celsius*0.8f,
                TemperatureUnit.Romer => celsius*21f/40f + 7.5f,
                TemperatureUnit.Custom => celsius*TemperatureMultiplier.Value + TemperatureAddend.Value,
                _ => celsius
            };
        }

        internal static void SetTemperatureThresholds()
        {
            PlayerHUD hud = CG.Game.Player.LocalPlayer.Instance?.gameObject?.GetComponentInChildren<PlayerHUD>();
            if (hud == null) return;
            LocalPlayerHUDView view = (LocalPlayerHUDView)viewField.GetValue(hud);
            if (view == null) return;
            HUDResourceVE temperature = (HUDResourceVE)temperatureField.GetValue(view);
            if (temperature == null) return;
            temperature.SetThresholds(new List<Tuple<int, string>>
            {
                new Tuple<int, string>((int)GetTemperature(-20), "normal"),
                new Tuple<int, string>((int)GetTemperature(-200), "medium"),
                new Tuple<int, string>((int)GetTemperature(-400), "low")
            });
        }

        internal static void Load(BepinPlugin plugin)
        {
            TemperatureUnitConfig = plugin.Config.Bind("FreedomUnits", "Temperature", TemperatureUnit.Celsius);
            TemperatureMultiplier = plugin.Config.Bind("FreedomUnits", "TemperatureMultiplier", 1f);
            TemperatureAddend = plugin.Config.Bind("FreedomUnits", "TemperatureAddend", 0f);
        }
    }
}
