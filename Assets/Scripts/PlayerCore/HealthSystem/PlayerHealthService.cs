using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiftGame.GameCore.GameLoop;
using LiftGame.GameCore.Pause;
using LiftGame.PlayerCore.MentalSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.HealthSystem
{
    public class PlayerHealthService : IPlayerHealthService, IPauseable
    {
        private readonly PlayerMentalData _playerMentalData;
        private readonly PlayerHealthData _playerHealthData;
        public event Action<int> OnDamaged = delegate { };
        public event Action OnPlayerDied = delegate { };
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

        public void AddDamage(int damage)
        {
            if (!_playerHealthData._isDamageable) return;
            _playerHealthData.Health -= damage;
            OnDamaged?.Invoke(damage);
        }

        public void SetHealthStartState()
        {
            _playerHealthData.Health = PlayerHealthData.MAX_HEALTH;
            SetMortal(true);
            EnableDamage(true);
            EnableStressDamage(true);
            UpdateHealthStatus();
            EnableHealthUpdate();
        }

        public void SetHealthSafeState()
        {
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
            await ApplyDamage(_cancellationToken.Token);
        }

        private async UniTask ApplyDamage(CancellationToken cancelToken)
        {
            while (_playerHealthData.IsDamageable)
            {
                if (_isPaused)
                {
                    Debug.Log( "paused");
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
                Debug.Log("HEALTH UPDATED");
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
            switch (_playerHealthData.Health)
            {
                case >= PlayerHealthData.MAX_HEALTH:
                    _playerHealthData.HealthStatus = HealthStatus.Stable;
                    break;
                case < PlayerHealthData.MAX_HEALTH and >= PlayerHealthData.MINOR_DAMAGE:
                    _playerHealthData.HealthStatus = HealthStatus.Stable;
                    break;
                case < PlayerHealthData.MINOR_DAMAGE and >= PlayerHealthData.MAJOR_DAMAGE:
                    _playerHealthData.HealthStatus = HealthStatus.MinorDamage;
                    break;
                case < PlayerHealthData.MAJOR_DAMAGE and >= PlayerHealthData.SEVERE_DAMAGE:
                    _playerHealthData.HealthStatus = HealthStatus.MajorDamage;
                    break;
                case < PlayerHealthData.SEVERE_DAMAGE and >= PlayerHealthData.CRITICAL_HEALTH:
                    _playerHealthData.HealthStatus = HealthStatus.Severe;
                    break;
                case < PlayerHealthData.CRITICAL_HEALTH and > PlayerHealthData.MIN_HEALTH:
                    _playerHealthData.HealthStatus = HealthStatus.Critical;
                    break;
                case <= PlayerHealthData.MIN_HEALTH:
                    _playerHealthData.HealthStatus = HealthStatus.Dead;
                    break;
                default:
                    _playerHealthData.HealthStatus = HealthStatus.Stable;
                    Debug.Log("Default HealthStatus is Set" + _playerHealthData.Health);
                    break;
            }
        }

        public bool IsPlayerDead()
        {
            if (!_playerHealthData.IsMortal) return false;
            if (_playerHealthData.HealthStatus == HealthStatus.Dead)
            {
                OnPlayerDied?.Invoke();
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
            if (_isPaused)
            {
                
            }
            else
            {
                
            }
        }
    }
}