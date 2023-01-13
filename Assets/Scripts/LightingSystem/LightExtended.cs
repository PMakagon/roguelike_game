﻿using System.Collections;
using LiftGame.FPSController.FirstPersonController;
using LiftGame.InteractableObjects.Electricals;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace LiftGame.LightingSystem
{
    public class LightExtended : MonoBehaviour //разбить на несколько классов компонентов
    {
        public enum LightMode 
        {
            Static,
            Blinking,
            Flickering
        }

        [SerializeField] private LightMode currentLightMode = LightMode.Static;
        [SerializeField] private Light _light;
        [SerializeField] private Light hotSpotLight;
        [SerializeField] private Renderer objectWithEmission;
        [SerializeField] private LensFlareComponentSRP lensFlare;

        [Header("Flickering Settings")] 
        [SerializeField] private float minIntensity = 0.3f;
        [SerializeField] private float maxIntensity = 10f;
        [SerializeField] private float noiseSpeed = 0.15f;

        [Header("Blinking Settings")] 
        [SerializeField] private float randomTimerValueMin = 5f;
        [SerializeField] private float randomTimerValueMax = 20f;

        [Space] [Header("Light Control")] 
        [SerializeField] private bool manualControl = false;
        [HideIf("manualControl")] 
        [SerializeField] private bool isPowered;
        [SerializeField] private bool isOn;
        [SerializeField] private MasterSwitcher masterSwitcher;
        [SerializeField] private SlaveSwitcher slaveSwitcher;
        
        
        [SerializeField] private bool isBroken;

        private float _startIntensity;
        private float _randomTimerValue;
        private float _startTimerValue = 0.01f;
        private Color _color;
        
        private MaterialPropertyBlock _matBlock;

        private void Awake()
        {
            _startIntensity = _light.intensity;
            if (objectWithEmission)
            {
                _matBlock = new MaterialPropertyBlock();
                _color = objectWithEmission.material.color;
            }
            if (slaveSwitcher)
            {
                slaveSwitcher.IsPowered = true;
            }
            ChangeState(IsStateChanged());
        }

        private bool IsStateChanged()
        {
            bool isActuallyOn;
            if (masterSwitcher)
            {
                isPowered = masterSwitcher.IsSwitchedOn; //если есть автомат то запитываемся от него
            }
            else
            {
                if (!manualControl)
                {
                    isPowered = true;  // если нет всегда запитано
                }
            }
            
            if (slaveSwitcher)
            {
                slaveSwitcher.IsPowered = isPowered; //если есть выключатель 
                isActuallyOn = slaveSwitcher.IsEnabled;
            }
            else
            {
                isActuallyOn = isPowered; // если нет то автомат управляет светом
            }
            
            if (!isPowered)
            {
                isActuallyOn = false;
            }
            return isActuallyOn;
        }

        public void SwitchState()
        {
            isPowered = !isPowered;
        }

        private void ChangeState(bool state)
        {
            _light.enabled = state;
            if (hotSpotLight)
            {
                hotSpotLight.enabled = state;
            }
            
            if (lensFlare)
            {
                lensFlare.enabled = state;
            }
            FetchEmission();
        }

        private bool GetLightComponentState()
        {
            return _light.enabled;
        }

        private void FetchEmission()
        {
            if (objectWithEmission)
            {
                objectWithEmission.GetPropertyBlock(_matBlock);
                if (_light.enabled)
                {
                    _matBlock.SetColor("_EmissiveColor", _color * _light.intensity);
                }
                else
                {
                    _matBlock.SetColor("_EmissiveColor", _color * 0f);
                }
                objectWithEmission.SetPropertyBlock(_matBlock);
            }
        }

        private void Update()
        {
            if (isBroken) return;
            bool state =  IsStateChanged();
            if (isOn != state)
            {
                isOn = state;
                ChangeState(isOn);
            }

            if (currentLightMode == LightMode.Blinking)
            {
                if (!isOn) return;
                StartCoroutine(Blinking());
            }

            if (currentLightMode == LightMode.Flickering)
            {
                if (isOn)
                {
                    _light.intensity = Mathf.Lerp(minIntensity, maxIntensity,
                        Mathf.PerlinNoise(10, Time.time / noiseSpeed));
                }
                FetchEmission();
            }
        }

        private IEnumerator Blinking()
        {
            ChangeState(isOn);
            yield return new WaitForSeconds(_startTimerValue);
            while (isOn)
            {
                _randomTimerValue = Random.Range(randomTimerValueMin, randomTimerValueMax);
                yield return new WaitForSeconds(_randomTimerValue);
                ChangeState(GetLightComponentState());
                ////ЗАТЫЧКА
                if (!isOn || currentLightMode != LightMode.Blinking)
                {
                    StopAllCoroutines();
                    FetchEmission();
                }
            }
        }

        #region Properties

        public bool IsBroken
        {
            get => isBroken;
            set => isBroken = value;
        }

        public Light Light
        {
            get => _light;
            set => _light = value;
        }

        public MasterSwitcher MasterSwitcher
        {
            get => masterSwitcher;
            set => masterSwitcher = value;
        }

        public SlaveSwitcher SlaveSwitcher
        {
            get => slaveSwitcher;
            set => slaveSwitcher = value;
        }

        public bool IsPowered
        {
            get => isPowered;
            set => isPowered = value;
        }

        public bool IsOn
        {
            get => isOn;
            set => isOn = value;
        }

        public LightMode LightModeP
        {
            get => currentLightMode;
            set => currentLightMode = value;
        }

        #endregion
    }
}