using FPSController.Scriptable_Objects;
using UnityEngine;

namespace PlayerEquipment
{
    public class HeadFlashlight : MonoBehaviour
    {
        [SerializeField] private InteractionInputData _interactionInputData;
        private Light _light;

        private void Awake()
        {
            _light = GetComponent<Light>();
        }

        private void Update()
        {
            if (_interactionInputData.FlashlightClicked)
            {
                _light.enabled = !_light.enabled;
            }
        }
    }
}