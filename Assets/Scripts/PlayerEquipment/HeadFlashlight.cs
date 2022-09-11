using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;

namespace LiftGame.PlayerEquipment
{
    public class HeadFlashlight : MonoBehaviour
    {
        [SerializeField] private float power = 1;
        [SerializeField] private float multiplier = 2f;
        [SerializeField] private bool isTurnedOn;
        private PlayerPowerData _playerPowerData;
        private Light _light;
        private bool _switchState;


        private void Awake()
        {
            _light = GetComponent<Light>();
            _light.enabled = isTurnedOn;
        }

        private void Update()
        {
            _light.enabled = isTurnedOn;
            if (!_playerPowerData.IsPowerOn)
            {
                isTurnedOn = false;
                return;
            }
            
            if (_switchState)
            {
                SwitchFlashlight();
            }
        }

        private void SwitchFlashlight()
        {
            _switchState = false;
            if (isTurnedOn)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
        }
        private void TurnOn()
        {
            isTurnedOn = true;
            _playerPowerData.CurrentPower -= power * multiplier;
            _playerPowerData.PowerLoad += power;
        }

        private void TurnOff()
        {
            isTurnedOn = false;
            _playerPowerData.PowerLoad -= power;
        }

        public bool SwitchState
        {
            get => _switchState;
            set => _switchState = value;
        }

        public PlayerPowerData PlayerPowerData
        {
            get => _playerPowerData;
            set => _playerPowerData = value;
        }

        public bool IsTurnedOn
        {
            get => isTurnedOn;
            set => isTurnedOn = value;
        }
    }
}