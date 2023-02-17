using LiftGame.GameCore.Input.Data;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerAirSystem;
using LiftGame.PlayerCore.PlayerPowerSystem;
using LiftGame.ProxyEventHolders;
using UnityEngine;
using Zenject;

namespace LiftGame.Ui.HUD
{
    public class HUDIndicationPanel : MonoBehaviour
    {
        [SerializeField] private GameObject signalIcon;
        [SerializeField] private GameObject bypassIcon;
        [SerializeField] private GameObject warningIcon;
        [SerializeField] private GameObject pressureIcon;
        private IPlayerAirService _airService;
        private IPlayerHealthService _healthService;
        private IPlayerPowerService _powerService;
        private IPlayerMentalService _mentalService;

        //MonoBehaviour injection
        [Inject]
        public void Construct(IPlayerAirService airService, IPlayerHealthService healthService,
            IPlayerPowerService powerService, IPlayerMentalService mentalService)
        {
            _airService = airService;
            _healthService = healthService;
            _powerService = powerService;
            _mentalService = mentalService;
        }

        private void Start()
        {
            EquipmentInputData.OnAirBypassClicked += UpdateBypassState;
            PlayerAirSupplyEventHolder.OnAirEmpty += ShowPressure;
            PlayerHealthEventHolder.OnHealthStatusChanged += ShowWarning;
            UpdateBypassState();
        }

        private void OnDestroy()
        {
            EquipmentInputData.OnAirBypassClicked -= UpdateBypassState;
        }

        private void UpdateBypassState()
        {
            bypassIcon.SetActive(_airService.IsBypassed());
        }

        
        private void ShowWarning(HealthStatus prevState,HealthStatus newState)
        {
            if (newState != HealthStatus.Critical) return;
            warningIcon.SetActive(true);
            PlayerHealthEventHolder.OnHealthStatusChanged -= ShowWarning;
            PlayerHealthEventHolder.OnHealthStatusChanged += HideWarning;

        }
        
        private void HideWarning(HealthStatus prevState,HealthStatus newState)
        {
            if (newState == HealthStatus.Severe && prevState == HealthStatus.Critical)
            {
                warningIcon.SetActive(false);
                PlayerHealthEventHolder.OnHealthStatusChanged += ShowWarning;
                PlayerHealthEventHolder.OnHealthStatusChanged -= HideWarning;
            }
            
        }
        
        private void ShowPressure()
        {
            pressureIcon.SetActive(true);
            PlayerAirSupplyEventHolder.OnAirRestoreApplied += HidePressure;
            PlayerAirSupplyEventHolder.OnAirLow -= ShowPressure;
        }
        
        private void HidePressure(PlayerAirData dummy,float x)
        {
            pressureIcon.SetActive(false);
            PlayerAirSupplyEventHolder.OnAirLow += ShowPressure;
            PlayerAirSupplyEventHolder.OnAirRestoreApplied -= HidePressure;
        }
    }
}