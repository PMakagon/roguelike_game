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
        private PowerData _powerData;
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
            if (!_powerData.IsPowerOn)
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
            _powerData.CurrentPower -= power * multiplier;
            _powerData.PowerLoad += power;
        }

        private void TurnOff()
        {
            _isTurnedOn = false;
            _powerData.PowerLoad -= power;
        }

        public bool SwitchState
        {
            get => _switchState;
            set => _switchState = value;
        }

        public PowerData PowerData
        {
            get => _powerData;
            set => _powerData = value;
        }

        public bool IsTurnedOn
        {
            get => _isTurnedOn;
            set => _isTurnedOn = value;
        }
    }
}