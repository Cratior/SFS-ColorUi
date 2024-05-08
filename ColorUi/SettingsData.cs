using UnityEngine;

namespace ColorUi
{
    public class SettingsData
    {
        public Color ThrottleminColor = RoundColorComponents(0.8f, 0.6f, 0.0f);
        public Color ThrottlemaxColor = RoundColorComponents(1f, 0f, 0f);
        public Color FuelminColor = RoundColorComponents(1f, 0.8f, 0.2f);
        public Color FuelmaxColor = RoundColorComponents(1f, 1f, 1f);
        public Color TempminColor = RoundColorComponents(0.9f, 0.5f, 0.1f);
        public Color TempmaxColor = RoundColorComponents(0.9f, 0.2f, 0.1f);

        private static Color RoundColorComponents(float r, float g, float b)
        {
            r = Mathf.Min(r, 1.0f);
            g = Mathf.Min(g, 1.0f);
            b = Mathf.Min(b, 1.0f);

            return new Color(
                Mathf.Round(r * 100) / 100,
                Mathf.Round(g * 100) / 100,
                Mathf.Round(b * 100) / 100
            );
        }
    }
}
