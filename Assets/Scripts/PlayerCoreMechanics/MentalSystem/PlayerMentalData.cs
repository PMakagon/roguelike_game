using UnityEngine;

namespace LiftGame.PlayerCoreMechanics.MentalSystem
{
    
    [CreateAssetMenu(fileName = "PlayerMentalData", menuName = "PlayerCoreMechanics/PlayerMentalData")]
    public class PlayerMentalData : ScriptableObject
    {
        // [SerializeField] private float currentStressModificator;
        // [SerializeField] private float baseStressModificator = 1;
        // [SerializeField] private float delay = 2f;
        // [SerializeField] private StressState stressState;
        //
        // [Header("STRESS MODS")]
        // [Slider(0f, 5f)]
        // [SerializeField] private float TotalDarknessMod = 3f;
        //
        // [Slider(0f, 3f)] 
        // [SerializeField] private float LightAheadMod = 0.5f;
        //
        // [Slider(0f, 2f)] 
        // [SerializeField] private float DarkAheadMod = 0.3f;
        //
        // [Slider(0f, 1f)] 
        // [SerializeField] private float InShadowMod = 0.1f;
        //
        // [Header("REGEN MODS")]
        // [Slider(0f, 2f)] 
        // [SerializeField] private float BaseRegen = 1f;
        //
        // [Slider(0f, 5f)] 
        // [SerializeField] private float FastRegen = 2f;
        //
        // public float StressLevel { get; set; }
        //
        // public const int MAX_STRESS_LEVEL = 220;
        // public const int MID_STRESS_LEVEL = 150;
        // public const int BASE_STRESS_LEVEL = 100;
        // public const int MIN_STRESS_LEVEL = 60;
        //
        //
        // public void SetStartState()
        // {
        //     StressLevel = BASE_STRESS_LEVEL;
        //     currentStressModificator = baseStressModificator;
        //     // UpdateStressState();
        // }
        
        // public void ReduceStressExposure()
        // {
        //     if (StressLevel<=MID_STRESS_LEVEL && StressLevel>BASE_STRESS_LEVEL)
        //     {
        //         StressLevel -= BaseRegen;
        //         return;
        //     }
        //
        //     if (StressLevel>MID_STRESS_LEVEL)
        //     {
        //         StressLevel -= FastRegen;
        //     }
        // }
        //
        // public void ApplyStressExposure()
        // {
        //     if (StressLevel<=MID_STRESS_LEVEL)
        //     {
        //         StressLevel += currentStressModificator;
        //     }
        //     switch (StressLevel)
        //     {
        //         case >= MAX_STRESS_LEVEL:
        //             StressLevel = MAX_STRESS_LEVEL;
        //             return;
        //         case <= MIN_STRESS_LEVEL:
        //             StressLevel = MIN_STRESS_LEVEL;
        //             return;
        //     }
        // }
        //
        // public void UpdateStressState()
        // {
        //     switch (StressLevel)
        //     {
        //         case >= MAX_STRESS_LEVEL:
        //             stressState = StressState.Max;
        //             break;
        //         case < BASE_STRESS_LEVEL and >= MIN_STRESS_LEVEL:
        //             stressState = StressState.Min;
        //             break;
        //         case < MAX_STRESS_LEVEL and >= MID_STRESS_LEVEL:
        //             stressState = StressState.Mid;
        //             break;
        //         case < MID_STRESS_LEVEL and >= BASE_STRESS_LEVEL:
        //             stressState = StressState.Base;
        //             break;
        //         default: stressState = StressState.Base;
        //             Debug.Log("Default StressState is Set");
        //             break;
        //     }
        // }
        
        // #region Properties
        //
        // public float CurrentStressModificator
        // {
        //     get => currentStressModificator;
        //     set => currentStressModificator = value;
        // }
        //
        // public float BaseStressModificator
        // {
        //     get => baseStressModificator;
        //     set => baseStressModificator = value;
        // }
        //
        // public StressState StressState
        // {
        //     get => stressState;
        //     set => stressState = value;
        // }
        //
        // public float Delay
        // {
        //     get => delay;
        //     set => delay = value;
        // }
        //
        // #endregion
    }
}