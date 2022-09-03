using System;
using Cysharp.Threading.Tasks;
using LiftGame.GameCore;
using LiftGame.GameCore.GameLoop;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.MentalSystem
{
   public class PlayerMentalService : IPlayerMentalService
    {
        private readonly PlayerMentalData _playerMentalData;
        public event Action<int> OnStressAdd = delegate { };
        
        [Inject]
        public PlayerMentalService (IPlayerData playerData)
        {
            _playerMentalData = playerData.GetMentalData();
            LevelGameLoopEventHandler.OnLoopStart += SetStartState;
            LevelGameLoopEventHandler.OnLoopEnd += SetSafeState;
        }

        public void SetStartState()
        {
            _playerMentalData.Stress = PlayerMentalData.BASE_STRESS_LEVEL;
            _playerMentalData.CurrentStressModificator = _playerMentalData.BaseStressModificator;
            EnableStressChange();
            UpdateStressState();
            Debug.Log("ENABLED STRESS");
        }

        public void SetSafeState()
        {
            _playerMentalData.Stress = PlayerMentalData.BASE_STRESS_LEVEL;
            _playerMentalData.CurrentStressModificator = _playerMentalData.BaseStressModificator;
            DisableStressChange();
            UpdateStressState();
        }

        public void AddStress(int stressAmount)
        {
            if (!_playerMentalData.IsEnabled) return;
            _playerMentalData.Stress += stressAmount;
            OnStressAdd.Invoke(stressAmount);

        }

        public void ReduceStress(int stressAmount)
        {
            if (_playerMentalData.Stress - stressAmount >= PlayerMentalData.MIN_STRESS_LEVEL)
            {
                _playerMentalData.Stress -= stressAmount;
            }
            else
            {
                _playerMentalData.Stress = PlayerMentalData.MIN_STRESS_LEVEL;
            }
        }

        public void EnableStressChange()
        {
            _playerMentalData.IsEnabled = true;
            UpdateStressExposure();
        }

        public void DisableStressChange()
        {
            _playerMentalData.IsEnabled = false;
        }

        private async void UpdateStressExposure()
        {
            while (_playerMentalData.IsEnabled)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_playerMentalData.UpdateTime), ignoreTimeScale: false);
                UpdateStressModificator();
                ApplyStressExposure();
                UpdateStressState();
                _playerMentalData.Stress = Mathf.RoundToInt(_playerMentalData.Stress);
                Debug.Log("STRESS UPDATED");
            }
        }

        private void ApplyStressExposure()
        {
            if (_playerMentalData.Stress <= PlayerMentalData.MID_STRESS_LEVEL)
            {
                _playerMentalData.Stress += _playerMentalData.CurrentStressModificator;
            }

            switch (_playerMentalData.Stress)
            {
                case >= PlayerMentalData.MAX_STRESS_LEVEL:
                    _playerMentalData.Stress = PlayerMentalData.MAX_STRESS_LEVEL;
                    return;
                case <= PlayerMentalData.MIN_STRESS_LEVEL:
                    _playerMentalData.Stress = PlayerMentalData.MIN_STRESS_LEVEL;
                    return;
            }
        }

        private void ReduceStressExposure()
        {
            if (_playerMentalData.Stress <= PlayerMentalData.MID_STRESS_LEVEL && _playerMentalData.Stress > PlayerMentalData.BASE_STRESS_LEVEL)
            {
                _playerMentalData.Stress -= _playerMentalData.BaseRegen;
                return;
            }

            if (_playerMentalData.Stress > PlayerMentalData.MID_STRESS_LEVEL)
            {
                _playerMentalData.Stress -= _playerMentalData.FastRegen;
            }
        }
        

        private void UpdateStressModificator()
        {
            var litStatus = _playerMentalData.PlayerLitState;
            switch (litStatus)
            {
                case PlayerLitState.Lit:
                    _playerMentalData.CurrentStressModificator = 0;
                    ReduceStressExposure();
                    return;
                case PlayerLitState.InTotalDark:
                    _playerMentalData.CurrentStressModificator = _playerMentalData.BaseStressModificator + _playerMentalData.TotalDarknessMod;
                    return;
                case PlayerLitState.InShadow:
                    _playerMentalData.CurrentStressModificator = _playerMentalData.BaseStressModificator + _playerMentalData.InShadowMod;
                    ReduceStressExposure();
                    return;
                case PlayerLitState.LightAhead:
                    _playerMentalData.CurrentStressModificator = _playerMentalData.BaseStressModificator + _playerMentalData.LightAheadMod;
                    ReduceStressExposure();
                    return;
                case PlayerLitState.DarkAhead:
                    _playerMentalData.CurrentStressModificator = _playerMentalData.BaseStressModificator + _playerMentalData.DarkAheadMod;
                    return;
                default:
                    _playerMentalData.CurrentStressModificator = _playerMentalData.BaseStressModificator;
                    break;
            }
        }

        private void UpdateStressState()
        {
            switch (_playerMentalData.Stress)
            {
                case >= PlayerMentalData.MAX_STRESS_LEVEL:
                    _playerMentalData.StressState = StressState.Max;
                    break;
                case < PlayerMentalData.BASE_STRESS_LEVEL and >= PlayerMentalData.MIN_STRESS_LEVEL:
                    _playerMentalData.StressState = StressState.Min;
                    break;
                case < PlayerMentalData.MAX_STRESS_LEVEL and >= PlayerMentalData.MID_STRESS_LEVEL:
                    _playerMentalData.StressState = StressState.Mid;
                    break;
                case < PlayerMentalData.MID_STRESS_LEVEL and >= PlayerMentalData.BASE_STRESS_LEVEL:
                    _playerMentalData.StressState = StressState.Base;
                    break;
                default:
                    _playerMentalData.StressState = StressState.Base;
                    Debug.Log("Default StressState is Set");
                    break;
            }
        }
        
    }
}