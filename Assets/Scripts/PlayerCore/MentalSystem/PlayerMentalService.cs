using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiftGame.GameCore.LevelGameLoop;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.ProxyEventHolders;
using LiftGame.ProxyEventHolders.Player;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.MentalSystem
{
   public class PlayerMentalService : IPlayerMentalService
    {
        private readonly PlayerMentalData _playerMentalData;
        private readonly PlayerHealthData _playerHealthData;
        private bool _isPaused;
        private CancellationTokenSource _cancellationToken = new();

        [Inject]
        public PlayerMentalService (IPlayerData playerData)
        {
            _playerMentalData = playerData.GetMentalData();
            _playerHealthData = playerData.GetHealthData();
            LevelGameLoopEventHandler.OnLoopStart += SetStartState;
            LevelGameLoopEventHandler.OnLoopEnd += SetSafeState;
        }

        public void SetStartState()
        {
            SubscribeToStressEvents();
            _playerMentalData.StressLevel = PlayerMentalData.BASE_STRESS_LEVEL;
            _playerMentalData.CurrentStressModificator = _playerMentalData.BaseStressModificator;
            EnableStressChange();
            UpdateStressState();
            Debug.Log("MENTAL SERVICE ENABLED");
        }

        public void SetSafeState()
        {
            UnsubscribeFromStressEvents();
            _playerMentalData.StressLevel = PlayerMentalData.BASE_STRESS_LEVEL;
            _playerMentalData.CurrentStressModificator = _playerMentalData.BaseStressModificator;
            DisableStressChange();
            UpdateStressState();
        }
        
        private void SubscribeToStressEvents()
        {
            PlayerMentalEventHolder.OnStressTaken += AddStress;
        } 
        private void UnsubscribeFromStressEvents()
        {
            PlayerMentalEventHolder.OnStressTaken -= AddStress;
        }

        public async void EnableStressChange()
        {
            _playerHealthData._isStressable = true;
            IsStressChangeEnabled = true;
            await UpdateStressExposure(_cancellationToken.Token);
        }

        public void DisableStressChange()
        {
            _playerHealthData._isStressable = false;
            IsStressChangeEnabled = false;
            _cancellationToken.Cancel();
        }

        private async UniTask UpdateStressExposure(CancellationToken cancelToken)
        {
            while (_playerHealthData._isStressable)
            {
                if (_isPaused)
                {
                    await UniTask.WaitUntil(() => _isPaused == false, cancellationToken: cancelToken);
                }
                
                if (!IsStressChangeEnabled)
                {
                    await UniTask.WaitUntil(() => IsStressChangeEnabled == true, cancellationToken: cancelToken);
                }
                
                await UniTask.Delay(TimeSpan.FromSeconds(_playerMentalData.UpdateTime), ignoreTimeScale: false);
                UpdateStressModificator();
                ApplyStressExposure();
                UpdateStressState();
                _playerMentalData.StressLevel = Mathf.RoundToInt(_playerMentalData.StressLevel);
            }
        }

        public bool IsStressChangeEnabled { get; private set; }
        public float GetStressLevel()
        {
            return _playerMentalData.StressLevel;
        }

        public StressState GetCurrentState()
        {
            return _playerMentalData.StressState;
        }

        public void AddStress(int stressAmount)
        {
            if (!_playerHealthData._isStressable) return;
            _playerMentalData.StressLevel += stressAmount;
            PlayerMentalEventHolder.BroadcastOnStressApplied(_playerMentalData,stressAmount);
        }

        public void ReduceStress(int stressAmount)
        {
            if (_playerMentalData.StressLevel - stressAmount >= PlayerMentalData.MIN_STRESS_LEVEL)
            {
                _playerMentalData.StressLevel -= stressAmount;
            }
            else
            {
                _playerMentalData.StressLevel = PlayerMentalData.MIN_STRESS_LEVEL;
            }
        }

        private void ApplyStressExposure()
        {
            if (_playerMentalData.StressLevel <= PlayerMentalData.MID_STRESS_LEVEL)
            {
                _playerMentalData.StressLevel += _playerMentalData.CurrentStressModificator;
            }

            switch (_playerMentalData.StressLevel)
            {
                case >= PlayerMentalData.MAX_STRESS_LEVEL:
                    _playerMentalData.StressLevel = PlayerMentalData.MAX_STRESS_LEVEL;
                    return;
                case <= PlayerMentalData.MIN_STRESS_LEVEL:
                    _playerMentalData.StressLevel = PlayerMentalData.MIN_STRESS_LEVEL;
                    return;
            }
        }

        private void ReduceStressExposure()
        {
            if (_playerMentalData.StressLevel <= PlayerMentalData.MID_STRESS_LEVEL && _playerMentalData.StressLevel > PlayerMentalData.BASE_STRESS_LEVEL)
            {
                _playerMentalData.StressLevel -= _playerMentalData.BaseRegen;
                return;
            }

            if (_playerMentalData.StressLevel > PlayerMentalData.MID_STRESS_LEVEL)
            {
                _playerMentalData.StressLevel -= _playerMentalData.FastRegen;
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
            StressState prevState = _playerMentalData.StressState;
            StressState newState;
            switch (_playerMentalData.StressLevel)
            {
                case >= PlayerMentalData.MAX_STRESS_LEVEL:
                    newState = StressState.Max;
                    break;
                case < PlayerMentalData.BASE_STRESS_LEVEL and >= PlayerMentalData.MIN_STRESS_LEVEL:
                    newState = StressState.Min;
                    break;
                case < PlayerMentalData.MAX_STRESS_LEVEL and >= PlayerMentalData.MID_STRESS_LEVEL:
                    newState = StressState.Mid;
                    break;
                case < PlayerMentalData.MID_STRESS_LEVEL and >= PlayerMentalData.BASE_STRESS_LEVEL:
                    newState = StressState.Base;
                    break;
                default:
                    newState = StressState.Base;
                    Debug.Log("Default StressState is Set");
                    break;
            }

            if (prevState!=newState)
            {
                PlayerMentalEventHolder.BroadcastOnStressStateChanged(prevState,newState);
            }

            _playerMentalData.StressState = newState;
        }

        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}