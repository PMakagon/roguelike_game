using System;
using System.Collections;
using LiftStateMachine;
using UnityEngine;
using NaughtyAttributes;

namespace PlayerCoreMechanics.MentalSystem
{
    public enum StressState
    {
        Min,
        Base,
        Mid,
        Max
    }
    public class PlayerMentalController : MonoBehaviour
    {
        [SerializeField] private PlayerMentalData playerMentalData;
        [SerializeField] private PlayerLightSensor playerSensor;
        [SerializeField] private PlayerLightSensor viewSensor;
        
        [SerializeField] private float currentStressModificator;
        [SerializeField] private float baseStressModificator = 1;
        [SerializeField] private StressState stressState;
        
        [SerializeField] private float delay = 2f;
        
        private float _stress;
        private int _playerLitLevel;
        
        public const int MAX_STRESS_LEVEL = 220;
        public const int MID_STRESS_LEVEL = 150;
        public const int BASE_STRESS_LEVEL = 100;
        public const int MIN_STRESS_LEVEL = 60;
        
        [Header("STRESS MODS")]
        [Slider(0f, 5f)]
        [SerializeField] private float TotalDarknessMod = 3f;
        [Slider(0f, 3f)] 
        [SerializeField] private float LightAheadMod = 0.5f;
        [Slider(0f, 2f)] 
        [SerializeField] private float DarkAheadMod = 0.3f;
        [Slider(0f, 1f)] 
        [SerializeField] private float InShadowMod = 0.1f;
        
        [Header("REGEN MODS")]
        [Slider(0f, 2f)] 
        [SerializeField] private float BaseRegen = 1f;
        [Slider(0f, 5f)] 
        [SerializeField] private float FastRegen = 2f;

        private bool _isTotalDark;
        private bool _isInLight;
        private bool _isDarkAhead;
        private bool _isLightAhead;
        private bool _isInShadow;

        private bool _isEnabled;

        public static Action onTriggerStress;

        private void Start()
        {
            _stress = BASE_STRESS_LEVEL;
            currentStressModificator = baseStressModificator;
            EnableStressChange();
            UpdateStressState();
            onTriggerStress = AddStress;
        }

        private void AddStress()
        {
            _stress += 40;
        }

        [ContextMenu("ENABLE_STRESS")]
        private void EnableStressChange()
        {
            if (!_isEnabled)
            {
                StartCoroutine(nameof(ChangeStressByTime));
            }
        }

        [ContextMenu("DISABLE_STRESS")]
        private void DisableStressChange()
        {
            _isEnabled = false;
            StopCoroutine(nameof(ChangeStressByTime));
        }

        private IEnumerator ChangeStressByTime()
        {
            _isEnabled = true;
            while (_isEnabled)
            {
                yield return new WaitForSecondsRealtime(delay);
                UpdateStressModificator();
                ApplyStressExposure();
                UpdateStressState();
                _stress = Mathf.RoundToInt(_stress);
            }
        }

        private void ApplyStressExposure() /////////////////
        {
            if (_stress<=MID_STRESS_LEVEL)
            {
                _stress += currentStressModificator;
            }
            switch (_stress)
            {
                case >= MAX_STRESS_LEVEL:
                    _stress = MAX_STRESS_LEVEL;
                    return;
                case <= MIN_STRESS_LEVEL:
                    _stress = MIN_STRESS_LEVEL;
                    return;
            }
        }
        
        private void ReduceStressExposure() /////////////////////
        {
            if (_stress<=MID_STRESS_LEVEL && _stress>BASE_STRESS_LEVEL)
            {
                _stress -= BaseRegen;
                return;
            }
        
            if (_stress>MID_STRESS_LEVEL)
            {
                _stress -= FastRegen;
            }
        }

        private void GetPlayerLightStatus()
        {
            int player = playerSensor._light;
            int view = viewSensor._light;
            _isTotalDark = player + view == 0;
            _isDarkAhead = player >= 10 && view <= 5;
            _isLightAhead = player == 0 && view >= 3;
            _isInShadow = player <= 10 && view <= 10;
            _isInLight = player >= 30 && view >= 10;
        }

        private void UpdateStressModificator()
        {
            GetPlayerLightStatus();
            if (_isInLight)
            {
                currentStressModificator = 0;
                ReduceStressExposure();
                return;
            }

            if (_isTotalDark)
            {
                currentStressModificator = baseStressModificator + TotalDarknessMod;
                return;
            }

            if (_isInShadow)
            {
                currentStressModificator = baseStressModificator + InShadowMod;
                ReduceStressExposure();
                return;
            }

            if (_isLightAhead)
            {
                currentStressModificator = baseStressModificator + LightAheadMod;
                ReduceStressExposure();
                return;
            }

            if (_isDarkAhead)
            {
                currentStressModificator = baseStressModificator + DarkAheadMod;
                return;
            }

            currentStressModificator = baseStressModificator;
        }

        private void UpdateStressState() ///////////
        {
            switch (_stress)
            {
                case >= MAX_STRESS_LEVEL:
                    stressState = StressState.Max;
                    break;
                case < BASE_STRESS_LEVEL and >= MIN_STRESS_LEVEL:
                    stressState = StressState.Min;
                    break;
                case < MAX_STRESS_LEVEL and >= MID_STRESS_LEVEL:
                    stressState = StressState.Mid;
                    break;
                case < MID_STRESS_LEVEL and >= BASE_STRESS_LEVEL:
                    stressState = StressState.Base;
                    break;
                default: stressState = StressState.Base;
                    Debug.Log("Default StressState is Set");
                    break;
            }
        }

        public float Stress
        {
            get => _stress;
            set => _stress = value;
        }

        public float CurrentStressModificator
        {
            get => currentStressModificator;
            set => currentStressModificator = value;
        }

        public StressState StressState
        {
            get => stressState;
            set => stressState = value;
        }
    }
}