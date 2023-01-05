using System.Collections;
using LiftGame.GameCore.Input.Data;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LiftGame.PlayerEquipment
{
    public class HeadFlashlight : MonoBehaviour
    {
        [SerializeField] private float power = 1;
        [SerializeField] private float multiplier = 2f;
        [SerializeField] private bool isTurnedOn;
        [SerializeField] private float maxAngle = 80f;
        [SerializeField] private float minAngle = 30f;
        private IPlayerPowerService _powerService;
        private EquipmentInputData _equipmentInputData;
        private Light _light;
        private bool _isBlinking;
        private bool _isAdjusted;

        public void Initialize(IPlayerPowerService playerPowerService, InputDataProvider inputDataProvider)
        {
            _powerService = playerPowerService;
            _equipmentInputData = inputDataProvider.EquipmentInputData;
        }

        private void Awake()
        {
            _light = GetComponent<Light>();
            _light.enabled = isTurnedOn;
        }

        private void Start()
        {
            _equipmentInputData.OnFlashlightClicked += SwitchFlashlightState;
            _equipmentInputData.OnFlashlightAdjust += AdjustFlashlightAngle;
            _powerService.OnPowerOff += TurnOffWithBlink;
        }

        private void OnDestroy()
        {
            _equipmentInputData.OnFlashlightClicked -= SwitchFlashlightState;
            _equipmentInputData.OnFlashlightAdjust -= AdjustFlashlightAngle;
            _powerService.OnPowerOff -= TurnOffWithBlink;
        }

        private void AdjustFlashlightAngle(float adjust)
        {
            if (!isTurnedOn) return;
            _light.spotAngle = Mathf.Clamp(_light.spotAngle += adjust * 10, minAngle, maxAngle);
            _light.range = Mathf.Clamp(_light.range -= adjust, 10, 15);
            _isAdjusted = true;
            
        }

        private void SwitchFlashlightState()
        {
            if (_isAdjusted)
            {
                _isAdjusted = false;
                return;
            }

            if (!_powerService.PlayerPowerData.IsPowerOn) return;
            if (isTurnedOn)
            {
                if (_isBlinking) StopCoroutine(BlinkFlashlight());
                _isBlinking = false;
                TurnOff();
            }
            else
            {
                if (!_isBlinking && _powerService.PlayerPowerData.CurrentPower < 10) StartCoroutine(BlinkFlashlight());
                TurnOn();
            }
        }

        private IEnumerator BlinkFlashlight()
        {
            _isBlinking = true;
            var blinkDuration = Random.Range(0.1f, 0.15f);
            float timer = 0;
            bool state = isTurnedOn;
            while (timer <= blinkDuration)
            {
                yield return new WaitForSecondsRealtime(Random.Range(0.01f, 0.1f));
                state = !state;
                _light.enabled = state;
                timer += Time.deltaTime;
            }

            _light.enabled = isTurnedOn;
            _isBlinking = false;
        }

        private void TurnOn()
        {
            isTurnedOn = true;
            _powerService.PlayerPowerData.CurrentPower -= power * multiplier;
            _powerService.PlayerPowerData.PowerLoad += power;
            _light.enabled = isTurnedOn;
        }

        private void TurnOff()
        {
            isTurnedOn = false;
            _powerService.PlayerPowerData.PowerLoad -= power;
            _light.enabled = isTurnedOn;
        }

        private void TurnOffWithBlink()
        {
            StartCoroutine(BlinkFlashlight());
            TurnOff();
        }

        public bool IsTurnedOn
        {
            get => isTurnedOn;
            set => isTurnedOn = value;
        }
    }
}