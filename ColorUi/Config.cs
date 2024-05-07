using System;
using UnityEngine;
using System.IO;

namespace ColorUI
{
    public class Config
    {
        [Serializable]
        public class SettingsData
        {
            public Color throttleMinColor = new Color(0.8f, 0.8f, 0.0f);
            public Color throttleMaxColor = new Color(1f, 0f, 0f);

            public Color fuelMinColor = new Color(1f, 0.92f, 0.016f);
            public Color fuelMaxColor = new Color(0f, 0.0f, 0.0f);

            public Color tempMinColor = new Color(0.9f, 0.5f, 0.1f);
            public Color tempMaxColor = new Color(1f, 0f, 0f);
        }

        private static string configFilePath = "config.txt";

        public static SettingsData LoadConfig()
        {
            SettingsData data = new SettingsData();

            if (!File.Exists(configFilePath))
            {
                CreateDefaultConfig();
            }
            else
            {
                string[] lines = File.ReadAllLines(configFilePath);
                if (lines.Length == 6)
                {
                    data.throttleMinColor = ParseColor(lines[0]);
                    data.throttleMaxColor = ParseColor(lines[1]);
                    data.fuelMinColor = ParseColor(lines[2]);
                    data.fuelMaxColor = ParseColor(lines[3]);
                    data.tempMinColor = ParseColor(lines[4]);
                    data.tempMaxColor = ParseColor(lines[5]);
                }
                else
                {
                    CreateDefaultConfig();
                }
            }

            return data;
        }

        private static void CreateDefaultConfig()
        {
            SettingsData defaultSettings = new SettingsData();
            SaveConfig(defaultSettings);
        }

        private static Color ParseColor(string line)
        {
            string[] components = line.Split(',');
            if (components.Length == 3 && float.TryParse(components[0], out float r) && float.TryParse(components[1], out float g) && float.TryParse(components[2], out float b))
            {
                return new Color(r, g, b);
            }
            else
            {
                return Color.white; // Default color
            }
        }

        public static void SaveConfig(SettingsData data)
        {
            string[] lines = new string[6];
            lines[0] = $"Throttle Min Color: {FormatColor(data.throttleMinColor)}";
            lines[1] = $"Throttle Max Color: {FormatColor(data.throttleMaxColor)}";
            lines[2] = $"Fuel Min Color: {FormatColor(data.fuelMinColor)}";
            lines[3] = $"Fuel Max Color: {FormatColor(data.fuelMaxColor)}";
            lines[4] = $"Temperature Min Color: {FormatColor(data.tempMinColor)}";
            lines[5] = $"Temperature Max Color: {FormatColor(data.tempMaxColor)}";
            File.WriteAllLines(configFilePath, lines);
        }


        private static string FormatColor(Color color)
        {
            return $"{color.r},{color.g},{color.b}";
        }
    }
}
