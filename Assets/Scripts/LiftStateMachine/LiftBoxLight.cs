using System;
using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class LiftBoxLight : MonoBehaviour
    {
        [SerializeField]private LiftControllerData _liftControllerData;
        private Light _light;
        private Color _standartColor;
        private Color _stopColor;
        private bool _isTurnedOn;
        [SerializeField] private float minIntensity = 0.2f;
        [SerializeField] private float maxIntensity = 10f;
        [SerializeField] private float noiseSpeed = 0.15f;
        [SerializeField]  private Material _materialWithEmission;
       
        private void Awake()
        {
            _light = GetComponent<Light>();
            _standartColor = _light.color;
            if (_materialWithEmission)
            {
                _standartColor = _materialWithEmission.color;
            }
        }
        private void FetchEmission()
        {
            if (_materialWithEmission)
            {
                if (_light.enabled)
                {
                    if (_liftControllerData.CurrentState.GetType() == typeof(StopState))
                    {
                        _materialWithEmission.SetColor("_EmissiveColor", Color.red * _light.intensity);
                    }
                    else
                    {
                        _materialWithEmission.SetColor("_EmissiveColor", _standartColor * _light.intensity);
                    }
                }
                else
                {
                    _materialWithEmission.SetColor("_EmissiveColor", _standartColor * 0f);
                }
            }
        }
        private void Update()
        {
            _isTurnedOn = _light.enabled;
            if (_liftControllerData.CurrentState.GetType() == typeof(StopState))
            {
                _light.color=Color.red;
            }
            else
            {
                _light.color = _standartColor;
            }
            
            if (_liftControllerData.CurrentState.GetType() == typeof(MovingState))
            {
                if (_isTurnedOn)
                {
                    _light.intensity = Mathf.Lerp(minIntensity, maxIntensity, 
                        Mathf.PerlinNoise(10, Time.time / noiseSpeed));
                }
            }
            FetchEmission();
        }
        
    }
}