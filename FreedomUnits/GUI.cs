﻿using VoidManager.CustomGUI;
using VoidManager.Utilities;
using static UnityEngine.GUILayout;

namespace FreedomUnits
{
    internal class GUI : ModSettingsMenu
    {
        private static string Multiplier = Configs.TemperatureMultiplier.Value.ToString();
        private static string Addend = Configs.TemperatureAddend.Value.ToString();

        public override string Name() => "Freedom Units";

        public override void Draw()
        {
            bool changed = false;
            changed |= DrawCheckbox("Celsius", Configs.TemperatureUnit.Celsius);
            changed |= DrawCheckbox("Fahrenheit", Configs.TemperatureUnit.Fahrenheit);
            changed |= DrawCheckbox("Kelvin", Configs.TemperatureUnit.Kelvin);
            changed |= DrawCheckbox("Rankine", Configs.TemperatureUnit.Rankine);
            changed |= DrawCheckbox("Delisle", Configs.TemperatureUnit.Delisle);
            changed |= DrawCheckbox("Reaumur", Configs.TemperatureUnit.Reaumur);
            changed |= DrawCheckbox("Romer", Configs.TemperatureUnit.Romer);
            BeginHorizontal();
            changed |= DrawCheckbox("Custom: ", Configs.TemperatureUnit.Custom);
            Label("Multiplier: ");
            Multiplier = TextField(Multiplier, MinWidth(80));
            Label("  Addend:");
            Addend = TextField(Addend, MinWidth(80));
            FlexibleSpace();
            if (Button("Apply"))
            {
                changed = true;
                if (float.TryParse(Multiplier, out float mul))
                {
                    Configs.TemperatureMultiplier.Value = mul;
                }
                if (float.TryParse(Addend, out float add))
                {
                    Configs.TemperatureAddend.Value = add;
                }
                Multiplier = Configs.TemperatureMultiplier.Value.ToString();
                Addend = Configs.TemperatureAddend.Value.ToString();
            }
            if (Button("Reset"))
            {
                changed = true;
                Configs.TemperatureMultiplier.Value = 1;
                Configs.TemperatureAddend.Value = 0;
                Multiplier = Configs.TemperatureMultiplier.Value.ToString();
                Addend = Configs.TemperatureAddend.Value.ToString();
            }
            EndHorizontal();

            if (changed)
            {
                Configs.SetTemperatureThresholds();
            }
        }

        private static bool DrawCheckbox(string name, Configs.TemperatureUnit unit)
        {
            bool refBool = Configs.TemperatureUnitConfig.Value == unit;
            if (GUITools.DrawCheckbox(name, ref refBool))
            {
                Configs.TemperatureUnitConfig.Value = unit;
                return true;
            }
            return false;
        }
    }
}
