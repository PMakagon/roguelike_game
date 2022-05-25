using System;
using System.Collections;
using FPSController;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LightingSystem
{
    public class LightExtended : MonoBehaviour
    {
        public enum LightType
        {
            Static,
            Blinking,
            Flickering
        }

        [SerializeField] private LightType _lightType = LightType.Static;
        [SerializeField] private Light _light;
        [SerializeField] private GameObject objectWithEmission;

        [Header("Flickering Settings")] [SerializeField]
        private float minIntensity = 0.3f;

        [SerializeField] private float maxIntensity = 10f;
        [SerializeField] private float noiseSpeed = 0.15f;

        [Header("Blinking Settings")] [SerializeField]
        private float randomTimerValueMIN = 5f;

        [SerializeField] private float randomTimerValueMAX = 20f;
        [SerializeField] private bool isActive;
        [SerializeField] public bool isOn;

        [Space] [Header("Light Control")] 
        [SerializeField] private MasterSwitcher masterSwitcher;
        [SerializeField] private SlaveSwitcher slaveSwitcher;

        private bool isBroken;

        private float startIntensity;
        private float randomTimerValue;
        private float startTimerValue = 0.01f;
        private Material _materialWithEmission;
        private Color _color;

        public Vector3 pointer;
        public float detectionRadius = 10.0f;
        public float detectionAngle = 90.0f;

        private void Awake()
        {
            _light = GetComponentInChildren<Light>();
            startIntensity = _light.intensity;
            if (objectWithEmission)
            {
                _materialWithEmission = objectWithEmission.GetComponent<Renderer>().material;
                _color = _materialWithEmission.color;
            }

            pointer = transform.position - Vector3.forward;
        }
        

        private IEnumerator Blinking()
        {
            _light.enabled = isOn;
            FetchEmission();
            yield return new WaitForSeconds(startTimerValue);
            while (isOn)
            {
                Debug.Log("WHILE IS ON");
                randomTimerValue = Random.Range(randomTimerValueMIN, randomTimerValueMAX);
                yield return new WaitForSeconds(randomTimerValue);
                _light.enabled = !_light.enabled;
                FetchEmission();
            }

            ////ЗАТЫЧКА
            if (!isOn || _lightType != LightType.Blinking)
            {
                Debug.Log("STOP");
                StopAllCoroutines();
                FetchEmission();
            }
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
            Vector3 lightPosition = transform.position;
            Vector3 toPlayer = FirstPersonController.Instance.transform.position - lightPosition;
            toPlayer.y = 0;
            if (toPlayer.magnitude <= detectionRadius)
            {
                if (Vector3.Dot(toPlayer.normalized, transform.forward) >
                    Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
                {
                    Debug.Log("Player has been detected!");
                }
            }
        }

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
                isActive = masterSwitcher.IsSwitchedOn;
            }

            if (slaveSwitcher)
            {
                isOn = slaveSwitcher.IsEnabled;
            }
            else
            {
                isOn = isActive;
            }

            if (!isActive)
            {
                isOn = false;
            }

            _light.enabled = isOn;


            if (_lightType == LightType.Static)
            {
                FetchEmission();
            }

            if (_lightType == LightType.Blinking)
            {
                if (!isOn) return;
                StartCoroutine(Blinking());
            }

            if (_lightType == LightType.Flickering)
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

        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        public bool IsOn
        {
            get => isOn;
            set => isOn = value;
        }

        public LightType LightTypeP
        {
            get => _lightType;
            set => _lightType = value;
        }

// #if UNITY_EDITOR
//         private void OnDrawGizmosSelected()
//         {
//             Color c = new Color(0.8f, 0, 0, 0.4f);
//             UnityEditor.Handles.color = c;
//
//             Vector3 rotatedForward = Quaternion.Euler(
//                 0,
//                 -detectionAngle * 0.5f,
//                 0) * transform.forward;
//             UnityEditor.Handles.DrawSolidDisc(pointer,gameObject.transform.forward,_light.range);
//             UnityEditor.Handles.DrawSolidArc(
//                 transform.position,
//                 Vector3.up,
//                 rotatedForward,
//                 detectionAngle,
//                 detectionRadius);
//
//         }
// #endif
    }
}