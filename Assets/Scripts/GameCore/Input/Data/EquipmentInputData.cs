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

        public static event Action OnFlashlightClicked = delegate { };
        public static event Action<float> OnFlashlightAdjust = delegate { };
        public static event Action OnTurnOnClicked = delegate { };
        public static event Action OnTurnOnReleased = delegate { };
        public static event Action OnAirBypassClicked = delegate { };
        public static event Action OnAirBypassReleased = delegate { };
        public static event Action OnUsingClicked = delegate { };
        public static event Action OnSwitchWeaponPressed = delegate { };

        public void UpdateInputEvents()
        {
            if (FlashlightClicked && ScrollWheelDirection != 0) OnFlashlightAdjust?.Invoke(ScrollWheelDirection);
            if (FlashlightReleased) OnFlashlightClicked?.Invoke();
            if (TurnOnClicked) OnTurnOnClicked?.Invoke();
            if (TurnOnReleased) OnTurnOnReleased?.Invoke();
            if (UsingClicked) OnUsingClicked?.Invoke();
            if (SwitchWeaponPressed) OnSwitchWeaponPressed?.Invoke();
            if (AirBypassClicked) OnAirBypassClicked?.Invoke();
            if (AirBypassReleased) OnAirBypassReleased?.Invoke();
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