using System;
using SFS.IO;
using SFS.UI.ModGUI;
using UITools;
using UnityEngine;

namespace ColorUi
{
    public class Settings : ModSettings<SettingsData>
    {
        protected override FilePath SettingsFile
        {
            get { return settingsPath; }
        }

        protected override void RegisterOnVariableChange(Action onChange)
        {
            Application.quitting += onChange;
        }

        private static FilePath settingsPath;

        public static void Init(FilePath path)
        {
            main = new Settings();
            settingsPath = path;
            main.Initialize();
            main.AddUI();
        }

        private void AddUI()
        {
            ConfigurationMenu.Add("Color UI Settings", new[]
            {
                new ValueTuple<string, Func<Transform, GameObject>>("Colors", (Transform transform) => ColorsUI(transform, ConfigurationMenu.ContentSize))
            });
        }

        private GameObject ColorsUI(Transform parent, Vector2Int size)
        {
            Box box = Builder.CreateBox(parent, size.x, size.y, 0, 0, 0.3f);
            box.CreateLayoutGroup(SFS.UI.ModGUI.Type.Vertical, TextAnchor.UpperLeft, 10f, null, true);

            Builder.CreateLabel(box, size.x - 50, 40, 0, 0, "ThrottleColors");
            // Throttle min color inputs
            Container minContainer = Builder.CreateContainer(box, 0, 0);
            minContainer.CreateLayoutGroup(SFS.UI.ModGUI.Type.Horizontal, TextAnchor.MiddleLeft, 0f, null, true);
            //Builder.CreateLabel(minContainer.rectTransform, 120, 40, 0, 0, "");
            InputWithLabel input_min = Builder.CreateInputWithLabel(minContainer.rectTransform, size.x - 20, 40, 0, 0, "min color: ", ColorToString(settings.ThrottleminColor), (input) =>
            {
                string[] components = input.Split(',');
                if (components.Length == 3)
                {
                    settings.ThrottleminColor.r = ParseFloat(components[0]);
                    settings.ThrottleminColor.g = ParseFloat(components[1]);
                    settings.ThrottleminColor.b = ParseFloat(components[2]);
                }
            });

            // Throttle max color inputs
            Container maxContainer = Builder.CreateContainer(box, 0, 0);
            maxContainer.CreateLayoutGroup(SFS.UI.ModGUI.Type.Horizontal, TextAnchor.MiddleLeft, 0f, null, true);
            //Builder.CreateLabel(maxContainer.rectTransform, 120, 40, 0, 0, "max color: ");
            InputWithLabel input_max = Builder.CreateInputWithLabel(maxContainer.rectTransform, size.x - 20, 40, 0, 0, "max color: ", ColorToString(settings.ThrottlemaxColor), (input) =>
            {
                string[] components = input.Split(',');
                if (components.Length == 3)
                {
                    settings.ThrottlemaxColor.r = ParseFloat(components[0]);
                    settings.ThrottlemaxColor.g = ParseFloat(components[1]);
                    settings.ThrottlemaxColor.b = ParseFloat(components[2]);
                }
            });

            Builder.CreateLabel(box, size.x - 50, 40, 0, 0, "FuelColors");
            // Fuel min color inputs
            Container minContainerFuel = Builder.CreateContainer(box, 0, 0);
            minContainerFuel.CreateLayoutGroup(SFS.UI.ModGUI.Type.Horizontal, TextAnchor.MiddleLeft, 0f, null, true);

            InputWithLabel input_min_fuel = Builder.CreateInputWithLabel(minContainerFuel.rectTransform, size.x - 20, 40, 0, 0, "min color: ", ColorToString(settings.FuelminColor), (input) =>
            {
                string[] components = input.Split(',');
                if (components.Length == 3)
                {
                    settings.FuelminColor.r = ParseFloat(components[0]);
                    settings.FuelminColor.g = ParseFloat(components[1]);
                    settings.FuelminColor.b = ParseFloat(components[2]);
                }
            });

            // Fuel max color inputs
            Container maxContainerFuel = Builder.CreateContainer(box, 0, 0);
            maxContainerFuel.CreateLayoutGroup(SFS.UI.ModGUI.Type.Horizontal, TextAnchor.MiddleLeft, 0f, null, true);
            //Builder.CreateLabel(maxContainerFuel.rectTransform, 120, 40, 0, 0, "max color: ");
            InputWithLabel input_max_fuel = Builder.CreateInputWithLabel(maxContainerFuel.rectTransform, size.x - 20, 40, 0, 0, "max color: ", ColorToString(settings.FuelmaxColor), (input) =>
            {
                string[] components = input.Split(',');
                if (components.Length == 3)
                {
                    settings.FuelmaxColor.r = ParseFloat(components[0]);
                    settings.FuelmaxColor.g = ParseFloat(components[1]);
                    settings.FuelmaxColor.b = ParseFloat(components[2]);
                }
            });

            Builder.CreateLabel(box, size.x - 50, 40, 0, 0, "TempretureColors");
            // Temperature min color inputs
            Container minContainerTemp = Builder.CreateContainer(box, 0, 0);
            minContainerTemp.CreateLayoutGroup(SFS.UI.ModGUI.Type.Horizontal, TextAnchor.MiddleLeft, 0f, null, true);
            //Builder.CreateLabel(minContainerTemp.rectTransform, 120, 40, 0, 0, "");
            InputWithLabel input_min_temp = Builder.CreateInputWithLabel(minContainerTemp.rectTransform, size.x - 20, 40, 0, 0, "min color: ", ColorToString(settings.TempminColor), (input) =>
            {
                string[] components = input.Split(',');
                if (components.Length == 3)
                {
                    settings.TempminColor.r = ParseFloat(components[0]);
                    settings.TempminColor.g = ParseFloat(components[1]);
                    settings.TempminColor.b = ParseFloat(components[2]);
                }
            });

            // Temperature max color inputs
            Container maxContainerTemp = Builder.CreateContainer(box, 0, 0);
            maxContainerTemp.CreateLayoutGroup(SFS.UI.ModGUI.Type.Horizontal, TextAnchor.MiddleLeft, 0f, null, true);
            //Builder.CreateLabel(maxContainerTemp.rectTransform, 120, 40, 0, 0, "max color: ");
            InputWithLabel input_max_temp = Builder.CreateInputWithLabel(maxContainerTemp.rectTransform, size.x - 20, 40, 0, 0, "max color: ", ColorToString(settings.TempmaxColor), (input) =>
            {
                string[] components = input.Split(',');
                if (components.Length == 3)
                {
                    settings.TempmaxColor.r = ParseFloat(components[0]);
                    settings.TempmaxColor.g = ParseFloat(components[1]);
                    settings.TempmaxColor.b = ParseFloat(components[2]);
                }
            });
            return box.gameObject;
        }

        private float ParseFloat(string input)
        {
            float value = 0f;
            float.TryParse(input, out value);
            return value;
        }
        private string ColorToString(Color color)
        {
            return $"{color.r},{color.g},{color.b}";
        }

        public static Settings main;
    }
}
