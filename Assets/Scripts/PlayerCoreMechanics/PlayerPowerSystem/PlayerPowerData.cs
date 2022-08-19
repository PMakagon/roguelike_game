using System;
using UnityEngine;

namespace LiftGame.PlayerCoreMechanics.PlayerPowerSystem
{
    [CreateAssetMenu(fileName = "PlayerPowerData", menuName = "PlayerPowerSystem/PowerData")]
    public class PlayerPowerData : ScriptableObject
    {
        [SerializeField] private float constLoad = 1f;
        
        [SerializeField]  private float _maxPower = 100f;
        
        private float _currentPower;

        private float _powerLoad;
        
        private float _minPower = 0;

        private bool _isPowerOn;

        public Action onPowerChange;

        // public Action onPowerOff;


        public bool IsNoPower() => CurrentPower <= _minPower;

        public void ChangePower()
        {
            if (!IsNoPower())
            {
                CurrentPower -= _powerLoad;
            }
            else
            {
                CurrentPower = _minPower;
                _isPowerOn = false;
            }

            if (_currentPower > _maxPower)
            {
                _currentPower = _maxPower;
            }
            onPowerChange?.Invoke();
        }

        public void ResetData()
        {
            CurrentPower = _maxPower;
            _powerLoad = constLoad;
            _isPowerOn = true;
            onPowerChange?.Invoke();
        }

        public bool IsPowerOn => _isPowerOn;

        public float CurrentPower
        {
            get => _currentPower;
            set => _currentPower = value;
        }

        public float PowerLoad
        {
            get => _powerLoad;
            set => _powerLoad = value;
        }
    }
}