using System;
using FPSController.Scriptable_Objects;
using PlayerPowerSystem;
using UnityEngine;

namespace PlayerEquipment
{
    public class HeadFlashlight : MonoBehaviour
    {
        [SerializeField] private float power = 1;
        [SerializeField] private float multiplier = 2f;
        private PlayerPowerData _playerPowerData;
        private Light _light;
        private bool _isTurnedOn;
        private bool _switchState;


        private void Awake()
        {
            _light = GetComponent<Light>();
            _light.enabled = _isTurnedOn;
        }

        private void Update()
        {
            _light.enabled = _isTurnedOn;
            if (!_playerPowerData.IsPowerOn)
            {
                _isTurnedOn = false;
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
            if (_isTurnedOn)
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
            _isTurnedOn = true;
            _playerPowerData.CurrentPower -= power * multiplier;
            _playerPowerData.PowerLoad += power;
        }

        private void TurnOff()
        {
            _isTurnedOn = false;
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
            get => _isTurnedOn;
            set => _isTurnedOn = value;
        }
    }
}