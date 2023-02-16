using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiftGame.GameCore.LevelGameLoop;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.ProxyEventHolders;
using LiftGame.ProxyEventHolders.Player;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.HealthSystem
{
    public class PlayerHealthService : IPlayerHealthService
    {
        private readonly PlayerMentalData _playerMentalData;
        private readonly PlayerHealthData _playerHealthData;
        private bool _isPaused;
        private CancellationTokenSource _cancellationToken = new();

        [Inject]
        public PlayerHealthService(IPlayerData playerData)
        {
            _playerMentalData = playerData.GetMentalData();
            _playerHealthData = playerData.GetHealthData();
            LevelGameLoopEventHandler.OnLoopStart += SetHealthStartState;
            LevelGameLoopEventHandler.OnLoopEnd += SetHealthSafeState;
        }

        private void SubscribeToHealthEvents()
        {
            PlayerHealthEventHolder.OnDamageTaken += AddDamage;
        } 
        private void UnsubscribeFromHealthEvents()
        {
            PlayerHealthEventHolder.OnDamageTaken -= AddDamage;
        }

        public void AddDamage(float damage)
        {
            if (!_playerHealthData._isDamageable) return;
            _playerHealthData.Health -= damage;
            PlayerHealthEventHolder.BroadcastOnDamageApplied(_playerHealthData,damage);
        }

        public void SetHealthStartState()
        {
            SubscribeToHealthEvents();
            _playerHealthData.Health = PlayerHealthData.MAX_HEALTH;
            SetMortal(true);
            EnableDamage(true);
            EnableStressDamage(true);
            UpdateHealthStatus();
            EnableHealthUpdate();
            Debug.Log("HEALTH SERVICE ENABLED");
        }

        public void SetHealthSafeState()
        {
            UnsubscribeFromHealthEvents();
            SetMortal(false);
            EnableDamage(false);
            EnableStressDamage(false);
            UpdateHealthStatus();
        }

        public void SetMortal(bool isMortal)
        {
            _playerHealthData.IsMortal = isMortal;
        }

        public void EnableDamage(bool isDamageAllowed)
        {
            _playerHealthData.IsDamageable = isDamageAllowed;
        }

        public void EnableStressDamage(bool isStressable)
        {
            _playerHealthData.IsStressable = isStressable;
        }

        private async void EnableHealthUpdate()
        {
            await UpdateHealth(_cancellationToken.Token);
        }

        private async UniTask UpdateHealth(CancellationToken cancelToken)
        {
            while (_playerHealthData.IsDamageable)
            {
                if (_isPaused)
                {
                    await UniTask.WaitUntil(() => _isPaused == false, cancellationToken: cancelToken);
                }

                if (IsPlayerDead())
                {
                    return;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_playerHealthData.UpdateTime), ignoreTimeScale: false,
                    cancellationToken: cancelToken);
                if (_playerHealthData.IsStressable)
                {
                    CheckStressStateAndDamage();
                }
                UpdateHealthStatus();
            }
        }

        private void CheckStressStateAndDamage() //to refactor
        {
            if (!_playerHealthData._isStressable) return;

            if (_playerHealthData.HealthStatus == HealthStatus.Dead) return;
            
            if (_playerMentalData.StressState == StressState.Max) 
            {
                if (_playerHealthData.HealthStatus != HealthStatus.Critical)
                {
                    _playerHealthData.Health -= _playerHealthData.MaxStressDamage;
                    return;
                }
            }

            if (_playerMentalData.StressState == StressState.Mid)
            {
                if (_playerHealthData.HealthStatus is HealthStatus.Stable or HealthStatus.MinorDamage)
                {
                    _playerHealthData.Health -= _playerHealthData.MidStressDamage;
                }
            }

            if (_playerMentalData.StressState == StressState.Min)
            {
                if (_playerHealthData.HealthStatus != HealthStatus.Stable)
                {
                    _playerHealthData.Health += _playerHealthData.HealthRegen * 2;
                }
            }

            if (_playerMentalData.StressState == StressState.Base)
            {
                if (_playerHealthData.HealthStatus != HealthStatus.Stable)
                {
                    _playerHealthData.Health += _playerHealthData.HealthRegen;
                }
            }
        }

        private void UpdateHealthStatus()
        {
            HealthStatus prevStatus = _playerHealthData.HealthStatus;
            HealthStatus newStatus;
            switch (_playerHealthData.Health)
            {
                case >= PlayerHealthData.MAX_HEALTH:
                    newStatus = HealthStatus.Stable;
                    break;
                case < PlayerHealthData.MAX_HEALTH and >= PlayerHealthData.MINOR_DAMAGE:
                    newStatus = HealthStatus.Stable;
                    break;
                case < PlayerHealthData.MINOR_DAMAGE and >= PlayerHealthData.MAJOR_DAMAGE:
                    newStatus = HealthStatus.MinorDamage;
                    break;
                case < PlayerHealthData.MAJOR_DAMAGE and >= PlayerHealthData.SEVERE_DAMAGE:
                    newStatus = HealthStatus.MajorDamage;
                    break;
                case < PlayerHealthData.SEVERE_DAMAGE and >= PlayerHealthData.CRITICAL_HEALTH:
                    newStatus = HealthStatus.Severe;
                    break;
                case < PlayerHealthData.CRITICAL_HEALTH and > PlayerHealthData.MIN_HEALTH:
                    newStatus = HealthStatus.Critical;
                    break;
                case <= PlayerHealthData.MIN_HEALTH:
                    newStatus = HealthStatus.Dead;
                    break;
                default:
                    newStatus = HealthStatus.Stable;
                    Debug.Log("Default HealthStatus is Set" + _playerHealthData.Health);
                    break;
            }
            
            if (prevStatus!=newStatus)
            {
                PlayerHealthEventHolder.BroadcastOnHealthStatusChanged(prevStatus,newStatus);
            }

            _playerHealthData.HealthStatus = newStatus;
        }

        public bool IsPlayerDead()
        {
            // if (!_playerHealthData.IsMortal) return false;
            if (_playerHealthData.HealthStatus == HealthStatus.Dead || _playerHealthData.Health <=0)
            {
                PlayerHealthEventHolder.BroadcastOnPlayerDied();
                _cancellationToken.Cancel();
                _cancellationToken = new CancellationTokenSource();
                Debug.Log("PLAYER IS DEAD");
                return true;
            }
            return false;
        }

        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}