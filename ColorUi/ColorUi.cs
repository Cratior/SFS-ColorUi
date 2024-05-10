using SFS.UI;
using SFS.World;
using UITools;
using UnityEngine;
using UnityEngine.UI;
using static SFS.World.Resources;

namespace ColorUi
{
    public class ColorUi : MonoBehaviour
    {
        private static SettingsData Settings
        {
            get
            {
                return ModSettings<SettingsData>.settings;
            }
        }

        private Throttle throttle;
        private ThrottleDrawer throttleDrawer;
        private AeroDrawer aeroDrawer;
        private ResourceBar[] resourceBars;
        private FuelTransferUI[] fuelTransferUIs;

        private Color previousThrottleColor;

        private void Awake()
        {
            FindUIComponents();
            InvokeRepeating(nameof(SlowUpdate), 0.0f, 0.4f);
        }

        private void FindUIComponents()
        {
            throttle = FindObjectOfType<Throttle>();
            throttleDrawer = FindObjectOfType<ThrottleDrawer>();
            aeroDrawer = FindObjectOfType<AeroDrawer>();
            resourceBars = FindObjectsOfType<ResourceBar>();
            fuelTransferUIs = FindObjectsOfType<FuelTransferUI>();

            if (throttle == null || throttleDrawer == null || aeroDrawer == null || resourceBars.Length == 0)
            {
                Debug.LogWarning("One or more UI components not found in the scene.");
            }
            Debug.Log("Updated UI components found.");
        }

        private void SlowUpdate()
        {
            ColorArrow();
            FuelBar();
            ColorTemperatureBars();
            if (throttle != null && throttleDrawer != null)
            {
                Rocket rocket = PlayerController.main.player.Value as Rocket;
                if (rocket != null && rocket.throttle != null)
                {
                    rocket.throttle.throttlePercent.OnChange += (oldValue, newValue) => { UpdateSliderColor(); };
                }
            }

        }
        private void UpdateSliderColor()
        {
            Rocket rocket = PlayerController.main.player.Value as Rocket;
            float throttlePercent = rocket?.throttle?.throttlePercent ?? 0.5f;
            Image fillImage = throttleDrawer.throttleSlider.GetComponentInChildren<Image>();
            if (fillImage != null)
            {
                Color color = Color.Lerp(Settings.ThrottleminColor, Settings.ThrottlemaxColor, throttlePercent);
                fillImage.color = color;
                throttleDrawer.throttlePercentText.Color = color;
            }
        }
        private void ColorArrow()
        {
            VelocityArrowDrawer arrowDrawer = FindObjectOfType<VelocityArrowDrawer>();

            if (arrowDrawer != null)
            {
                Color color = CalculateArrowColor(arrowDrawer);

                arrowDrawer.velocity_X.line.color = color;
                SetArrowColor(arrowDrawer.velocity_X, color);
                SetArrowColor(arrowDrawer.velocity_Y, color);
            }
        }

        private void SetArrowColor(VelocityArrowDrawer.Arrow arrow, Color color)
        {
            arrow.line.color = color;
            arrow.text.color = color;
            arrow.line_Shadow.color = color;
            arrow.text_Shadow.color = color;
            arrow.line.transform.parent.GetChild(0).gameObject.GetComponent<Image>().color = color;
            arrow.line.transform.GetChild(1).gameObject.GetComponent<Image>().color = color;
        }

        private Color CalculateArrowColor(VelocityArrowDrawer arrowDrawer)
        {
            float xVelocity = Mathf.Abs(arrowDrawer.velocity_X.holder.position.x);
            float yVelocity = Mathf.Abs(arrowDrawer.velocity_Y.holder.position.y);

            Color xColor = Color.Lerp(Color.blue, Color.red, Mathf.InverseLerp(0f, 500f, xVelocity));
            Color yColor = Color.Lerp(Color.magenta, Color.green, Mathf.InverseLerp(0f, 500f, yVelocity));

            float totalVelocity = xVelocity + yVelocity;
            float xWeight = xVelocity / totalVelocity;
            float yWeight = yVelocity / totalVelocity;

            Color finalColor = xColor * xWeight + yColor * yWeight;

            return finalColor;
        }

        private void FuelBar()
        {
            foreach (ResourceBar bar in resourceBars)
            {
                Color color = Color.Lerp(Settings.FuelminColor, Settings.FuelmaxColor, bar.bar.fillAmount);
                bar.bar.color = color;
                bar.percentText.Color = color;
            }

            foreach (FuelTransferUI fuelTransferUI in fuelTransferUIs)
            {
                float fuelPercent = fuelTransferUI.resourceBar.fillAmount;
                Color color = Color.Lerp(Settings.FuelminColor, Settings.FuelmaxColor, fuelPercent);
                fuelTransferUI.resourceBar.color = color;
                fuelTransferUI.percentText.Color = color;
            }
        }

        private void ColorTemperatureBars()
        {
            if (aeroDrawer == null) return;

            foreach (TemperatureBar bar in aeroDrawer.bars)
            {
                Color color = Color.Lerp(Settings.TempminColor, Settings.TempmaxColor, bar.bar.fillAmount);
                bar.bar.color = color;
                bar.temperatureDegree.Color = color;
            }
        }
    }
}
