using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PlayerCoreMechanics.MentalSystem;
using UnityEngine;

namespace PlayerCoreMechanics.HealthSystem
{
    public enum HealthStatus
    {
        Stable,
        MinorDamage,
        MajorDamage,
        Critical,
        Dead
    }
    
    public class PlayerHealthController : MonoBehaviour
    {
        [SerializeField] private PlayerMentalController playerMentalController;
        [SerializeField] private PlayerHealthData healthData;
        // private List<HealthCondition> _conditions;
        private HealthStatus _healthStatus;
        private float _health;
        
        [Header("STRESS DAMAGE")]
        [Slider(0f, 30f)]
        [SerializeField] private float maxStressDamage = 3f;
        [Slider(0f, 30f)] 
        [SerializeField] private float midStressDamage = 0.5f;
        [Slider(0f, 30f)] 
        [SerializeField] private float healthRegen = 0.1f;

        public const int MAX_HEALTH = 100;
        public const int MINOR_DAMAGE = 80;
        public const int MAJOR_DAMAGE = 50;
        public const int CRITICAL_HEALTH = 30;
        public const int MIN_HEALTH = 0;

        public bool isMortal = true;
        public bool isStressable = true;
        
        private void Start()
        {
            _health = MAX_HEALTH;
            UpdateHealthStatus();
            EnableHealthUpdate();
        }

        private void Update()
        {
            HandleDeath();
        }

        private void HandleDeath()
        {
            if (_healthStatus==HealthStatus.Dead)
            {
                Debug.Log("PLAYER IS DEAD");
            }
        }
        
        private void EnableHealthUpdate()
        {
            if (isMortal)
            {
                StartCoroutine(nameof(DamageByStress));
            }
        }

        [ContextMenu("DISABLE_STRESSABBILITY")]
        private void DisableStressChange()
        {
            isStressable = false;
            StopCoroutine(nameof(DamageByStress));
        }
        
        private IEnumerator DamageByStress()
        {
            while (isStressable)
            {
                yield return new WaitForSecondsRealtime(0.5f);
                CheckAndApplyStressState();
                UpdateHealthStatus();
            }
        }
        
        private void CheckAndApplyStressState()
        {
            if (_healthStatus == HealthStatus.Dead)
            {
                return;
            }

            if (playerMentalController.StressState==StressState.Max) //switch?
            {
                if (_healthStatus != HealthStatus.Critical)
                {
                    _health -= maxStressDamage;
                    return;
                }
            }
            
            if (playerMentalController.StressState==StressState.Mid)
            {
                if (_healthStatus is HealthStatus.Stable or HealthStatus.MinorDamage)
                {
                    _health -= midStressDamage;
                }
            }
            
            if (playerMentalController.StressState==StressState.Min)
            {
                if (_healthStatus != HealthStatus.Stable)
                {
                    _health += healthRegen*2;
                }
                else
                {
                    _health = MAX_HEALTH;
                }
            }
            
            if (playerMentalController.StressState==StressState.Base)
            {
                if (_healthStatus != HealthStatus.Stable)
                {
                    _health += healthRegen;
                }
                else
                {
                    _health = MAX_HEALTH;
                }

            }
        }
        
       

        private void UpdateHealthStatus()
        {
            switch (_health)
            {
                case >= MAX_HEALTH:
                    _healthStatus = HealthStatus.Stable;
                    break;
                case <= CRITICAL_HEALTH and > MIN_HEALTH:
                    _healthStatus = HealthStatus.Critical;
                    break;
                case < MAX_HEALTH and >= MINOR_DAMAGE:
                    _healthStatus = HealthStatus.MinorDamage;
                    break;
                case < MINOR_DAMAGE and >= MAJOR_DAMAGE:
                    _healthStatus = HealthStatus.MajorDamage;
                    break;
                case < MIN_HEALTH:
                    _healthStatus = HealthStatus.Dead;
                    break;
                default: _healthStatus = HealthStatus.Stable;
                    Debug.Log("Default HealthStatus is Set");
                    break;
            }
        }

        public HealthStatus HealthStatus
        {
            get => _healthStatus;
            set => _healthStatus = value;
        }

        public float Health
        {
            get => _health;
            set => _health = value;
        }
    }
}