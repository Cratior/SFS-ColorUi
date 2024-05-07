using UnityEngine;
using UnityEngine.UI;
using SFS.World;
using SFS.Parts.Modules;
using SFS.UI;
using SFS.Variables;
using System.Collections.Generic;
using SFS.World.Drag;
using System;
using ModLoader.Helpers;
using ColorUI;

namespace ColorUi
{
    public class ThrottleSliderColorUpdater : MonoBehaviour
    {
        public Color ThrottleminColor = new Color(0.8f, 0.8f, 0.0f);
        public Color ThrottlemaxColor = new Color(1f, 0f, 0f);

        public Color FuelminColor = Color.yellow;
        public Color FuelmaxColor = Color.white;

        public Color TempminColor = new Color(0.9f, 0.5f, 0.1f);
        public Color TempmaxColor = Color.red;

        private Throttle throttle;
        private ThrottleDrawer throttleDrawer;
        private AeroDrawer aeroDrawer;
        private ResourceBar resourceBar;

        private void Awake()
        {
            FindUIComponents();

        }

        private void FindUIComponents()
        {
            throttle = FindObjectOfType<Throttle>();
            throttleDrawer = FindObjectOfType<ThrottleDrawer>();
            aeroDrawer = FindObjectOfType<AeroDrawer>();
            resourceBar = FindObjectOfType<SFS.UI.ResourceBar>();

            if (throttle == null || throttleDrawer == null || aeroDrawer == null || resourceBar == null)
            {
                Debug.LogWarning("One or more UI components not found in the scene.");
            }
        }

        private void Update()
        {
            FindUIComponents();

            if (throttle != null && throttleDrawer != null)
            {
                float throttlePercent = throttle.throttlePercent.Value;
                UpdateSliderColor(throttlePercent);
                float fillAmount = Mathf.Lerp(0.16f, 0.84f, throttlePercent);
                throttleDrawer.throttleSlider.SetFillAmount(fillAmount, false);
                UpdateUIThemeColors();
            }
        }

        private void UpdateSliderColor(float throttlePercent)
        {
            Image fillImage = throttleDrawer.throttleSlider.GetComponentInChildren<Image>();
            if (fillImage != null)
            {
                Color color = Color.Lerp(ThrottleminColor, ThrottlemaxColor, throttlePercent);
                fillImage.color = color;
            }
            ColorArrow();
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
        }

        private Color CalculateArrowColor(VelocityArrowDrawer arrowDrawer)
        {
            //float xVelocity = Mathf.Abs(arrowDrawer.velocity_X.holder.position.x);
            //float yVelocity = Mathf.Abs(arrowDrawer.velocity_Y.holder.position.y);

            //Color xColor = Color.Lerp(Color.blue, Color.red, Mathf.InverseLerp(0f, 500f, xVelocity));
            //Color yColor = Color.Lerp(Color.magenta, Color.green, Mathf.InverseLerp(0f, 500f, yVelocity));

            //float totalVelocity = xVelocity + yVelocity;
            //float xWeight = xVelocity / totalVelocity;
            //float yWeight = yVelocity / totalVelocity;

            //Color finalColor = xColor * xWeight + yColor * yWeight;

            //return finalColor;
            return Color.white;
        }

        private void UpdateUIThemeColors()
        {
            fuelbar();
            ColorTemperatureBars();
        }

        private void fuelbar()
        {
            Color color = Color.Lerp(FuelminColor, FuelmaxColor, resourceBar.bar.fillAmount);
            resourceBar.bar.color = color;
        }

        private void ColorTemperatureBars()
        {
            foreach (TemperatureBar bar in aeroDrawer.bars)
            {
                Color color = Color.Lerp(TempminColor, TempmaxColor, bar.bar.fillAmount);
                bar.bar.color = color;
            }
        }
    }
}
