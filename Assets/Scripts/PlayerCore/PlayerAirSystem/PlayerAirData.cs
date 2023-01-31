using UnityEngine;

namespace LiftGame.PlayerCore.PlayerAirSystem
{
    [CreateAssetMenu(fileName = "PlayerAirData", menuName = "PlayerCoreMechanics/PlayerAirData")]
    public class PlayerAirData : ScriptableObject
    {
        [SerializeField] private float healthDamageOnEmpty = 2f;
        [SerializeField] private float maxStressUsage = 2f;
        [SerializeField] private float midStressUsage = 1f;
        [SerializeField] private float baseStressUsage = 0.5f;
        [SerializeField] private float minStressUsage = 0.2f;
        [SerializeField] private float updateTime = 0.5f;
        
        public readonly int MAX_AIR = 100;
        public readonly int MIN_AIR = 0;

        public bool IsEmpty() => CurrentAirLevel <= MIN_AIR;

        public float CurrentAirLevel { get; set; }

        public float CurrentAirUsage { get; set; }
        public bool IsActive { get; set; }
        public bool IsBypassed { get; set; }
        public float HealthDamageOnEmpty => healthDamageOnEmpty;
        public float MaxStressUsage => maxStressUsage;

        public float MidStressUsage => midStressUsage;

        public float BaseStressUsage => baseStressUsage;

        public float MinStressUsage => minStressUsage;

        public float UpdateTime => updateTime;
        
        
        
        public void ResetData()
        {
            CurrentAirLevel = MAX_AIR;
            CurrentAirUsage = baseStressUsage;
            IsBypassed = false;
        }
    }
}