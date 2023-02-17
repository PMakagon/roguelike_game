using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.LevelGameLoop;
using LiftGame.GameCore.Pause;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.ProxyEventHolders;
using ModestTree;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.PlayerAirSystem
{
    public class PlayerAirService : IPlayerAirService
    {
        private IPlayerMentalService _mentalService;
        private IPlayerHealthService _healthService;
        private PlayerAirData _airData;
        private CancellationTokenSource _cancellationToken = new();
        private StressState _lastState;
        private bool _isPaused;

        [Inject]
        public PlayerAirService(IPlayerData playerData, IPlayerMentalService mentalService,
            IPlayerHealthService healthService, IPauseHandler pauseHandler)
        {
            _airData = playerData.GetAirData();
            _mentalService = mentalService;
            _healthService = healthService;
            LevelGameLoopEventHandler.OnLoopStart += EnableAirSupply;
            LevelGameLoopEventHandler.OnLoopEnd += DisableAirSupply;
            EquipmentInputData.OnAirBypassClicked += ChangeBypassState;
        }

        private void ChangeBypassState()
        {
            _airData.IsBypassed = !_airData.IsBypassed;
        }

        public bool IsBypassed()
        {
            return _airData.IsBypassed;
        }

        private void SubscribeToAirEvents()
        {
            PlayerAirSupplyEventHolder.OnAirRestored += AddAir;
        }

        private void UnsubscribeFromAirEvents()
        {
            PlayerAirSupplyEventHolder.OnAirRestored -= AddAir;
        }

        public void EnableAirSupply()
        {
            _airData.IsActive = true;
            SubscribeToAirEvents();
            EnableAirUpdate();
            PlayerAirSupplyEventHolder.BroadcastOnAirUsageChanged(_airData);
            Debug.Log("AIR SERVICE ENABLED");
        }

        public void DisableAirSupply()
        {
            _airData.IsActive = false;
            UnsubscribeFromAirEvents();
        }

        public void AddAir(float airToAdd)
        {
            _airData.CurrentAirLevel += airToAdd;
            PlayerAirSupplyEventHolder.BroadcastOnAirRestoreApplied(_airData, airToAdd);
        }

        public float GetCurrentLevel()
        {
            return _airData.CurrentAirLevel;
        }

        public float GetCurrentUsage()
        {
            return _airData.CurrentAirUsage;
        }

        public bool IsEmpty()
        {
            return _airData.IsEmpty();
        }

        public void ResetAirData()
        {
            _airData.ResetData();
        }

        private async void EnableAirUpdate()
        {
            await UpdateAirReduce(_cancellationToken.Token);
        }

        private async UniTask UpdateAirReduce(CancellationToken cancelToken)
        {
            while (_airData.IsActive)
            {
                if (_isPaused)
                {
                    await UniTask.WaitUntil(() => _isPaused == false, cancellationToken: cancelToken);
                }
                await UniTask.Delay(TimeSpan.FromSeconds(_airData.UpdateTime), ignoreTimeScale: false,
                    cancellationToken: cancelToken);
                if (_airData.IsBypassed) continue;
                if (_airData.IsEmpty())
                {
                    ApplyLowAirDamage();
                }
                else
                {
                    ApplyAirUsage();
                }
            }
        }

        private void ApplyLowAirDamage()
        {
            _healthService.AddDamage(_airData.HealthDamageOnEmpty);
        }

        private void ApplyAirUsage()
        {
            if (!_airData.IsActive) return;
            var stressState = _mentalService.GetCurrentState();
            if (_lastState != stressState)
            {
                _airData.CurrentAirUsage = stressState switch
                {
                    StressState.Max => _airData.MaxStressUsage,
                    StressState.Mid => _airData.MidStressUsage,
                    StressState.Base => _airData.BaseStressUsage,
                    StressState.Min => _airData.MinStressUsage,
                    _ => throw new ArgumentOutOfRangeException()
                };
                _lastState = stressState;
                PlayerAirSupplyEventHolder.BroadcastOnAirUsageChanged(_airData);
            }

            _airData.CurrentAirLevel -= _airData.CurrentAirUsage;
            PlayerAirSupplyEventHolder.BroadcastOnAirLevelChanged(_airData);
            if (_airData.CurrentAirLevel <= _airData.MAX_AIR * 0.2 && _airData.CurrentAirLevel >_airData.MAX_AIR * 0.1 )/////
            {
                PlayerAirSupplyEventHolder.BroadcastOnAirLow();
            }
            if (!_airData.IsEmpty()) return;
            PlayerAirSupplyEventHolder.BroadcastOnAirEmpty();
        }


        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}