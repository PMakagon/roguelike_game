using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiftGame.PlayerCore.MentalSystem;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.HealthSystem
{
    [CreateAssetMenu(fileName = "PlayerHealthData", menuName = "PlayerCoreMechanics/HealthData")]
    public class PlayerHealthData : ScriptableObject
    {
        private HealthStatus _healthStatus;
        private float _health;

        [Header("STRESS DAMAGE")] 
        [Slider(0f, 30f)] [SerializeField] private float maxStressDamage = 3f;
        [Slider(0f, 30f)] [SerializeField] private float midStressDamage = 0.5f;
        [Slider(0f, 30f)] [SerializeField] private float healthRegen = 0.1f;
        [SerializeField] private float updateTime = 0.5f;
        
        public const int MAX_HEALTH = 100;
        public const int MINOR_DAMAGE = 95;
        public const int MAJOR_DAMAGE = 75;
        public const int SEVERE_DAMAGE = 50;
        public const int CRITICAL_HEALTH = 20;
        public const int MIN_HEALTH = 0;

        //make private
        public bool _isMortal;
        public bool _isDamageable;
        public bool _isStressable;

        public void ResetData()
        {
            _healthStatus = HealthStatus.Stable;
            _health = MAX_HEALTH;
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

        public float MaxStressDamage => maxStressDamage;

        public float MidStressDamage => midStressDamage;

        public float HealthRegen => healthRegen;

        public float UpdateTime => updateTime;

        public bool IsMortal
        {
            get => _isMortal;
            set => _isMortal = value;
        }

        public bool IsDamageable
        {
            get => _isDamageable;
            set => _isDamageable = value;
        }

        public bool IsStressable
        {
            get => _isStressable;
            set => _isStressable = value;
        }
    }
}