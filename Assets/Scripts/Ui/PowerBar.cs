using System;
using System.Globalization;
using LiftGame.PlayerCore.PlayerPowerSystem;
using LiftGame.ProxyEventHolders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.Ui
{
    public class PowerBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentLoad;
        [SerializeField] private TextMeshProUGUI currentPower;
        [SerializeField] private TextMeshProUGUI currentMaxPower;
        [SerializeField] private Slider slider;
        [SerializeField] private Image sliderFill;
        [SerializeField] private Image sliderBorder;
        [SerializeField] private Image icon;
        [SerializeField] private Color lowColor;
        [SerializeField] private Color defaultColor;
        private bool _isLow;

        private void Start()
        {
            PlayerPowerEventHolder.OnMaxPowerChanged += UpdatePowerBar;
            PlayerPowerEventHolder.OnMaxPowerChanged += UpdateMaxPowerText;
            PlayerPowerEventHolder.OnPowerChanged += UpdatePowerBar;
            PlayerPowerEventHolder.OnLoadChanged += UpdatePowerLoadText;
            PlayerPowerEventHolder.OnPowerOn += FetchColor;
            PlayerPowerEventHolder.OnPowerOff += FetchColor;
        }

        private void OnDestroy()
        { 
            PlayerPowerEventHolder.OnMaxPowerChanged -= UpdatePowerBar;
            PlayerPowerEventHolder.OnMaxPowerChanged -= UpdateMaxPowerText;
            PlayerPowerEventHolder.OnPowerChanged -= UpdatePowerBar;
            PlayerPowerEventHolder.OnLoadChanged -= UpdatePowerLoadText;
            PlayerPowerEventHolder.OnPowerOn -= FetchColor;
            PlayerPowerEventHolder.OnPowerOff -= FetchColor;
        }

        private void UpdatePowerBar(PlayerPowerData powerData)
        {
            currentPower.text = powerData.CurrentPower.ToString(CultureInfo.InvariantCulture);
            var power = powerData.CurrentPower/10;
            slider.value = power;
            var isLowNow = power <= 2;
            if (_isLow == isLowNow) return;
            _isLow = isLowNow;
            FetchColor();
        }

        private void UpdatePowerLoadText(PlayerPowerData powerData)
        {
            currentLoad.text =Math.Round(powerData.PowerLoad,2).ToString(CultureInfo.InvariantCulture);
        } 
        private void UpdateMaxPowerText(PlayerPowerData powerData)
        {
            currentMaxPower.text =Math.Round(powerData.MaxPower,2).ToString(CultureInfo.InvariantCulture);
            currentMaxPower.color = powerData.MaxPower==0? Color.red : Color.white;
        }

        private void FetchColor()
        {
            sliderFill.color = _isLow? lowColor : defaultColor;
            sliderBorder.color = _isLow? lowColor : defaultColor;
            icon.color = _isLow? Color.red : Color.white;
        }
    }
}