using System;
using System.Collections;
using FPSController;
using FPSController.First_Person_Controller;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace LightingSystem
{
    public class LightExtended : MonoBehaviour
    {
        public enum LightMode // StateMode
        {
            Static,
            Blinking,
            Flickering
        }

        [SerializeField] private LightMode _currentLightMode = LightMode.Static;
        [SerializeField] private Light _light;
        [SerializeField] private Light _hotSpotLight;
        [SerializeField] private GameObject objectWithEmission;
        [SerializeField] private LensFlareComponentSRP lensFlare;

        [Header("Flickering Settings")] 
        [SerializeField] private float minIntensity = 0.3f;
        [SerializeField] private float maxIntensity = 10f;
        [SerializeField] private float noiseSpeed = 0.15f;

        [Header("Blinking Settings")] 
        [SerializeField] private float randomTimerValueMIN = 5f;
        [SerializeField] private float randomTimerValueMAX = 20f;

        [Space] [Header("Light Control")] 
        [SerializeField] private bool directControl = false;
        [ShowIf("directControl")] [SerializeField] private bool isPowered;
        [SerializeField] private bool isOn;
        [SerializeField] private MasterSwitcher masterSwitcher;
        [SerializeField] private SlaveSwitcher slaveSwitcher;
        
        
        [SerializeField] private bool isBroken;

        private float _startIntensity;
        private float _randomTimerValue;
        private float _startTimerValue = 0.01f;
        private Material _materialWithEmission;
        private Color _color;

        private void Awake()
        {
            // _light = GetComponentInChildren<Light>();
            _startIntensity = _light.intensity;
            if (objectWithEmission)
            {
                _materialWithEmission = objectWithEmission.GetComponent<Renderer>().material;
                _color = _materialWithEmission.color;
            }
            if (slaveSwitcher)
            {
                slaveSwitcher.IsPowered = true;
            }
        }
        
        private IEnumerator Blinking()
        {
            ChangeState(isOn);
            yield return new WaitForSeconds(_startTimerValue);
            while (isOn)
            {
                _randomTimerValue = Random.Range(randomTimerValueMIN, randomTimerValueMAX);
                yield return new WaitForSeconds(_randomTimerValue);
                ChangeState(GetState());
                ////ЗАТЫЧКА
                if (!isOn || _currentLightMode != LightMode.Blinking)
                {
                    StopAllCoroutines();
                    FetchEmission();
                }
            }
        }

        public void SwitchState()
        {
            isPowered = !isPowered;
        }

        private void ChangeState(bool state)
        {
            _light.enabled = state;
            if (_hotSpotLight)
            {
                _hotSpotLight.enabled = state;
            }
            
            if (lensFlare)
            {
                lensFlare.enabled = state;
            }
            FetchEmission();
        } 
        
        public bool GetState()
        {
            return _light.enabled;
        }

        private void FetchEmission()
        {
            if (objectWithEmission)
            {
                if (_light.enabled)
                {
                    _materialWithEmission.SetColor("_EmissiveColor", _color * _light.intensity);
                }
                else
                {
                    _materialWithEmission.SetColor("_EmissiveColor", _color * 0f);
                }
            }
        }

        public void LookForPlayer()
        {
            bool playerSpotted = false;
            Vector3 lightPosition = transform.position;
            Vector3 toPlayer = FirstPersonController.Instance.transform.position - lightPosition;
            toPlayer.y = 0;
            if (toPlayer.magnitude <= _light.range*0.9)
            {
                playerSpotted = true;
                Debug.DrawRay(lightPosition,toPlayer,playerSpotted ? Color.green : Color.red);
                // if (Vector3.Dot(toPlayer.normalized, transform.forward) >
                //     Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
                // {
                //     Debug.Log("Player has been detected!");
                // }
            }
        }

        //Причесать
        private void Update()
        {
            if (isBroken)
            {
                return;
            }
            
            if (isOn)
            {
                LookForPlayer();
            }

            if (masterSwitcher)
            {
                isPowered = masterSwitcher.IsSwitchedOn; //если есть автомат то запитываемся от него
            }
            else
            {
                if (!directControl)
                {
                    isPowered = true;  // если нет всегда запитано
                }
            }

            if (slaveSwitcher)
            {
                isOn = slaveSwitcher.IsEnabled;
                slaveSwitcher.IsPowered = isPowered; //если есть выключатель 
            }
            else
            {
                isOn = isPowered; // если нет то автомат управляет светом
            }

            if (!isPowered)
            {
                isOn = false;
            }
            
            ChangeState(isOn);
            
            if (_currentLightMode == LightMode.Static)
            {
                FetchEmission();
            }

            if (_currentLightMode == LightMode.Blinking)
            {
                if (!isOn) return;
                StartCoroutine(Blinking());
            }

            if (_currentLightMode == LightMode.Flickering)
            {
                if (isOn)
                {
                    _light.intensity = Mathf.Lerp(minIntensity, maxIntensity,
                        Mathf.PerlinNoise(10, Time.time / noiseSpeed));
                }
                FetchEmission();
            }
        }

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
            get => _currentLightMode;
            set => _currentLightMode = value;
        }
    }
}