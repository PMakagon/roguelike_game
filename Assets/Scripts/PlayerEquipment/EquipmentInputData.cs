using UnityEngine;

namespace PlayerEquipment
{
    [CreateAssetMenu(fileName = "EquipmentInputData", menuName = "FirstPersonController/Data/EquipmentInputData")]
    public class EquipmentInputData : ScriptableObject
    {
        private bool _usingClicked;
        private bool _usingReleased;

        private bool _flashlightClicked;
        private bool _flashlightReleased;

        private bool _turnOnClicked;
        private bool _turnOnReleased;

        private bool _visorUpClicked;
        private bool _visorUpReleased;

        private bool _airBypassClicked;
        private bool _airBypassReleased;
        
        private bool _firstEquipmentClicked;
        private bool _firstEquipmentReleased;
        
        private bool _secondEquipmentClicked;
        private bool _secondEquipmentReleased;
        
        private bool _thirdEquipmentClicked;
        private bool _thirdEquipmentReleased;
        
        private bool _fourthEquipmentClicked;
        private bool _fourthEquipmentReleased;
        

        public bool UsingClicked
        {
            get => _usingClicked;
            set => _usingClicked = value;
        }

        public bool UsingReleased
        {
            get => _usingReleased;
            set => _usingReleased = value;
        }

        public bool FlashlightClicked
        {
            get => _flashlightClicked;
            set => _flashlightClicked = value;
        }

        public bool FlashlightReleased
        {
            get => _flashlightReleased;
            set => _flashlightReleased = value;
        }

        public bool TurnOnClicked
        {
            get => _turnOnClicked;
            set => _turnOnClicked = value;
        }

        public bool TurnOnReleased
        {
            get => _turnOnReleased;
            set => _turnOnReleased = value;
        }

        public bool VisorUpClicked
        {
            get => _visorUpClicked;
            set => _visorUpClicked = value;
        }

        public bool VisorUpReleased
        {
            get => _visorUpReleased;
            set => _visorUpReleased = value;
        }

        public bool AirBypassClicked
        {
            get => _airBypassClicked;
            set => _airBypassClicked = value;
        }

        public bool AirBypassReleased
        {
            get => _airBypassReleased;
            set => _airBypassReleased = value;
        }

        public bool FirstEquipmentClicked
        {
            get => _firstEquipmentClicked;
            set => _firstEquipmentClicked = value;
        }

        public bool FirstEquipmentReleased
        {
            get => _firstEquipmentReleased;
            set => _firstEquipmentReleased = value;
        }

        public bool SecondEquipmentClicked
        {
            get => _secondEquipmentClicked;
            set => _secondEquipmentClicked = value;
        }

        public bool SecondEquipmentReleased
        {
            get => _secondEquipmentReleased;
            set => _secondEquipmentReleased = value;
        }

        public bool ThirdEquipmentClicked
        {
            get => _thirdEquipmentClicked;
            set => _thirdEquipmentClicked = value;
        }

        public bool ThirdEquipmentReleased
        {
            get => _thirdEquipmentReleased;
            set => _thirdEquipmentReleased = value;
        }

        public bool FourthEquipmentClicked
        {
            get => _fourthEquipmentClicked;
            set => _fourthEquipmentClicked = value;
        }

        public bool FourthEquipmentReleased
        {
            get => _fourthEquipmentReleased;
            set => _fourthEquipmentReleased = value;
        }

        public void ResetInput()
        {
            _usingClicked = false;
            _usingReleased = false;
            _flashlightClicked = false;
            _flashlightReleased = false;
            _turnOnClicked = false;
            _turnOnReleased = false;
            _visorUpClicked = false;
            _visorUpReleased = false;
            _airBypassClicked = false;
            _airBypassReleased = false;
        }
    }
}