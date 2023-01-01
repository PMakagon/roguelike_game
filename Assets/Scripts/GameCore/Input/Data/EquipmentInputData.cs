using System;
using UnityEngine;

namespace LiftGame.GameCore.Input.Data
{
    [CreateAssetMenu(fileName = "EquipmentInputData", menuName = "PlayerInputData/EquipmentInputData")]
    public class EquipmentInputData : ScriptableObject
    {
        public void ResetInput()
        {
            UsingClicked = false;
            UsingReleased = false;
            FlashlightClicked = false;
            FlashlightReleased = false;
            TurnOnClicked = false;
            TurnOnReleased = false;
            VisorUpClicked = false;
            VisorUpReleased = false;
            AirBypassClicked = false;
            AirBypassReleased = false;
            SwitchWeaponPressed = false;
        }

        public event Action OnFlashlightClicked = delegate { };
        public event Action<float> OnFlashlightAdjust = delegate { };
        public event Action OnTurnOnClicked = delegate { };
        public event Action OnTurnOnReleased = delegate { };
        public event Action OnUsingClicked = delegate { };
        public event Action OnSwitchWeaponPressed = delegate { };

        public void UpdateInputEvents()
        {
            if (FlashlightClicked && ScrollWheelDirection != 0) OnFlashlightAdjust?.Invoke(ScrollWheelDirection);
            if (FlashlightReleased) OnFlashlightClicked?.Invoke();
            if (TurnOnClicked) OnTurnOnClicked?.Invoke();
            if (TurnOnReleased) OnTurnOnReleased?.Invoke();
            if (UsingClicked) OnUsingClicked?.Invoke();
            if (SwitchWeaponPressed) OnSwitchWeaponPressed?.Invoke();
        }

        public float ScrollWheelDirection { get; set; }

        public bool UsingClicked { get; set; }

        public bool UsingReleased { get; set; }

        public bool FlashlightClicked { get; set; }

        public bool FlashlightReleased { get; set; }

        public bool TurnOnClicked { get; set; }

        public bool TurnOnReleased { get; set; }

        public bool SwitchWeaponPressed { get; set; }

        public bool SwitchWeaponReleased { get; set; }

        public bool VisorUpClicked { get; set; }

        public bool VisorUpReleased { get; set; }

        public bool AirBypassClicked { get; set; }

        public bool AirBypassReleased { get; set; }

        public bool FirstEquipmentClicked { get; set; }

        public bool FirstEquipmentReleased { get; set; }

        public bool SecondEquipmentClicked { get; set; }

        public bool SecondEquipmentReleased { get; set; }

        public bool ThirdEquipmentClicked { get; set; }

        public bool ThirdEquipmentReleased { get; set; }

        public bool FourthEquipmentClicked { get; set; }

        public bool FourthEquipmentReleased { get; set; }
    }
}