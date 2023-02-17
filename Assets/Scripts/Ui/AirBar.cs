using System;
using System.Globalization;
using LiftGame.PlayerCore.PlayerAirSystem;
using LiftGame.ProxyEventHolders;
using LiftGame.ProxyEventHolders.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LiftGame.Ui
{
    public class AirBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentUsage;
        [SerializeField] private TextMeshProUGUI currentAir;
        [SerializeField] private Slider slider;
        [SerializeField] private Image sliderFill;
        [SerializeField] private Image icon;
        [SerializeField] private Color lowColor;
        [SerializeField] private Color defaultColor;
        private bool _isLow;
        private bool _isEmpty;
        
        private void Start()
        {
            PlayerAirSupplyEventHolder.OnAirLevelChanged += UpdateAirBar;
            PlayerAirSupplyEventHolder.OnAirUsageChanged += UpdateAirUsageText;
        }

        private void OnDestroy()
        {
            PlayerAirSupplyEventHolder.OnAirLevelChanged -= UpdateAirBar;
            PlayerAirSupplyEventHolder.OnAirUsageChanged -= UpdateAirUsageText;
        }

        private void UpdateAirBar(PlayerAirData playerAirData)
        {
            currentAir.text = playerAirData.CurrentAirLevel.ToString(CultureInfo.InvariantCulture);
            var airLevel = playerAirData.CurrentAirLevel / 10;
            slider.value = airLevel;
            var isLowNow = airLevel <= 2;
            if (_isLow == isLowNow) return;
            _isLow = isLowNow;
            ChangeColor();
        }

        private void UpdateAirUsageText(PlayerAirData playerAirData)
        {
            currentUsage.text = Math.Round(playerAirData.CurrentAirUsage, 2).ToString(CultureInfo.InvariantCulture);
        }

        private void ChangeColor()
        {
            sliderFill.color = _isLow ? lowColor : defaultColor;
            icon.color = _isLow ? Color.red : Color.white;
        }
    }
}