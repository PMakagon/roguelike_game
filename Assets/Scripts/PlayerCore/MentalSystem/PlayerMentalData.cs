using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.PlayerCore.MentalSystem
{
    [CreateAssetMenu(fileName = "PlayerMentalData", menuName = "PlayerCoreMechanics/PlayerMentalData")]
    public class PlayerMentalData : ScriptableObject
    {
        [SerializeField] private PlayerLitState playerLitState; //temp
        [SerializeField] private float currentStressModificator;
        [SerializeField] private float baseStressModificator = 1;
        [SerializeField] private StressState stressState;
        [SerializeField] private float updateTime = 2f;

        [Header("DARKNESS STRESS MODS")] 
        [MinMaxSlider(0f, 5f)] [SerializeField] private float totalDarknessMod = 3f;
        [MinMaxSlider(0f, 3f)] [SerializeField] private float lightAheadMod = 0.5f;
        [MinMaxSlider(0f, 2f)] [SerializeField] private float darkAheadMod = 0.3f;
        [MinMaxSlider(0f, 1f)] [SerializeField] private float inShadowMod = 0.1f;

        [Header("REGEN MODS")] 
        [MinMaxSlider(0f, 2f)] [SerializeField] private float baseRegen = 1f;
        [MinMaxSlider(0f, 5f)] [SerializeField] private float fastRegen = 2f;

        [Header("CONSTANTS")] 
        public const int MAX_STRESS_LEVEL = 220;
        public const int MID_STRESS_LEVEL = 150;
        public const int BASE_STRESS_LEVEL = 100;
        public const int MIN_STRESS_LEVEL = 60;

        private float _stress;
        private int _playerLitLevel;

        public void ResetData()
        {
            stressState = StressState.Base;
            _stress = BASE_STRESS_LEVEL;
        }
        
        public PlayerLitState PlayerLitState
        {
            get => playerLitState;
            set => playerLitState = value;
        }

        public float CurrentStressModificator
        {
            get => currentStressModificator;
            set => currentStressModificator = value;
        }

        public float BaseStressModificator
        {
            get => baseStressModificator;
            set => baseStressModificator = value;
        }

        public StressState StressState
        {
            get => stressState;
            set => stressState = value;
        }

        public float UpdateTime => updateTime;

        public float TotalDarknessMod => totalDarknessMod;

        public float LightAheadMod => lightAheadMod;

        public float DarkAheadMod => darkAheadMod;

        public float InShadowMod => inShadowMod;

        public float BaseRegen => baseRegen;

        public float FastRegen => fastRegen;

        public float Stress
        {
            get => _stress;
            set => _stress = value;
        }

        public int PlayerLitLevel
        {
            get => _playerLitLevel;
            set => _playerLitLevel = value;
        }
        
    }
}