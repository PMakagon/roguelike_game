using System;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.PlayerPowerSystem
{
    [CreateAssetMenu(fileName = "PlayerPowerData", menuName = "PlayerCoreMechanics/PowerData")]
    public class PlayerPowerData : ScriptableObject
    {
        [SerializeField] private float constLoad = 1f;
        [SerializeField] private float maxPower = 100f;
        [SerializeField] private float reduceRate = 1f;
        private float _currentPower;
        private float _powerLoad;
        private readonly float _minPower = 0;
        private bool _isPowerOn;
        public float ConstLoad => constLoad;

        public void ResetData()
        {
            _currentPower = maxPower;
        }

        public float MaxPower
        {
            get => maxPower;
            set => maxPower = value;
        }

        public float ReduceRate => reduceRate;

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

        public float MinPower => _minPower;

        public bool IsPowerOn
        {
            get => _isPowerOn;
            set => _isPowerOn = value;
        }
    }
}