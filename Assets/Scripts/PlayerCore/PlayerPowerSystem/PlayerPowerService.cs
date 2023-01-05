using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiftGame.PlayerCore.MentalSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.PlayerPowerSystem
{
    class PlayerPowerService : IPlayerPowerService
    {
        private readonly PlayerPowerData _playerPowerData;
        public event Action OnPowerChange = delegate { };
        public event Action OnPowerOff = delegate { };

        [Inject]
        public PlayerPowerService (IPlayerData playerData)
        {
            _playerPowerData = playerData.GetPowerData();
        }

        public void SetActive(bool state)
        {
            _playerPowerData.IsPowerOn = state;
            if (state) UpdatePowerReduce();
        }

        public void AddPower(int powerToAdd)
        {
            _playerPowerData.CurrentPower += powerToAdd;
            HandleOverload();
        }

        public bool IsNoPower() => _playerPowerData.CurrentPower <= _playerPowerData.MinPower;

        public void ReducePowerLoad()
        {
            if (!_playerPowerData.IsPowerOn) return;
            if (!IsNoPower())
            {
                _playerPowerData.CurrentPower -= _playerPowerData.PowerLoad;
            }
            if (IsNoPower())
            {
                _playerPowerData.IsPowerOn = false;
                OnPowerOff?.Invoke();
            }
            OnPowerChange?.Invoke();
        }

        private void HandleOverload()
        {
            if (_playerPowerData.CurrentPower > _playerPowerData.MaxPower)
            {
                _playerPowerData.CurrentPower = _playerPowerData.MaxPower;
            }
        }

        private async void UpdatePowerReduce()
        {
            while (_playerPowerData.IsPowerOn)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_playerPowerData.ReduceRate), ignoreTimeScale: false);
                ReducePowerLoad();
            }
        }

        public void ResetPowerData()
        {
            _playerPowerData.CurrentPower = _playerPowerData.MaxPower;
            _playerPowerData.PowerLoad = _playerPowerData.ConstLoad;
            _playerPowerData.IsPowerOn = true;
            OnPowerChange?.Invoke();
        }
        
        public PlayerPowerData PlayerPowerData => _playerPowerData;
    }
}