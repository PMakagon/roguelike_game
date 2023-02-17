using System;
using System.Collections;
using LiftGame.InteractableObjects.Electricals;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace LiftGame.LevelPowerSystem
{
    public class LightExtended : MonoBehaviour //to full refactor
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

        [Header("Flickering Settings")] [SerializeField]
        private float minIntensity = 0.3f;

        [SerializeField] private float maxIntensity = 10f;
        [SerializeField] private float noiseSpeed = 0.15f;

        [Header("Blinking Settings")] [SerializeField]
        private float randomTimerValueMin = 5f;

        [SerializeField] private float randomTimerValueMax = 20f;

        [Space] [Header("Light Control")] 
        [SerializeField] private bool isAlwaysOn = false;
        [SerializeField] private bool isOn;
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
            if (slaveSwitcher) slaveSwitcher.OnSwitch += ChangeState;
            ChangeState(CheckState());
        }

        private void OnDestroy()
        {
            if (slaveSwitcher) slaveSwitcher.OnSwitch -= ChangeState;
        }

        private bool CheckState()
        {
            if (isBroken) return false;
            if (isAlwaysOn) return true;
            if (slaveSwitcher) return slaveSwitcher.IsEnabled;
            return isOn;
        }

        //for interactions
        public void ChangeState()
        {
            ChangeState(!isOn);
        }

        private void ChangeState(bool state)
        {
            if (isBroken) return;
            if (isAlwaysOn) state = true;
            isOn = state;
            _light.enabled = state;
            if (hotSpotLight) hotSpotLight.enabled = state;
            if (lensFlare) lensFlare.enabled = state;
            FetchEmission();
            if (currentLightMode != LightMode.Blinking) return;
            if (isOn) StartCoroutine(Blinking());
        }

        private void FetchEmission()
        {
            if (!objectWithEmission) return;
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

        private void Update()
        {
            if (currentLightMode != LightMode.Flickering) return;
            if (isOn)
            {
                _light.intensity = Mathf.Lerp(minIntensity, maxIntensity,
                    Mathf.PerlinNoise(10, Time.time / noiseSpeed));
            }
            FetchEmission();
        }

        private IEnumerator Blinking()
        {
            ChangeState(isOn);
            yield return new WaitForSeconds(_startTimerValue);
            while (isOn)
            {
                _randomTimerValue = Random.Range(randomTimerValueMin, randomTimerValueMax);
                yield return new WaitForSeconds(_randomTimerValue);
                ChangeState(_light.enabled);
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

        public SlaveSwitcher SlaveSwitcher
        {
            get => slaveSwitcher;
            set => slaveSwitcher = value;
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