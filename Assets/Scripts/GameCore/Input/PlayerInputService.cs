using LiftGame.GameCore.Input.Data;
using UnityEngine;
using Zenject;

namespace LiftGame.GameCore.Input
{
    public class PlayerInputService : IPlayerInputService
    {
        private InputDataProvider _inputData;
        private bool _isInputActive;

        [Inject]
        public void Construct(InputDataProvider inputData)
        {
            _inputData = inputData;
        }

        public void Initialize(InputDataProvider inputDataProvider)
        {
            _inputData = inputDataProvider;
            ResetInput();
        }
        
        public void UpdateInput()
        {
            UpdateNonGameplayInputData();
            if (!_isInputActive) return;
            UpdateUiInputData();
            UpdateCameraInput();
            UpdateMovementInputData();
            UpdateInteractionInputData();
            UpdateEquipmentInputData();
        }

        public void SetInputActive(bool state)
        {
            if (state==false) ResetInput();
            _isInputActive = state;
        }

        private void UpdateNonGameplayInputData()
        {
            _inputData.NonGameplayInputData.PauseMenuClicked = UnityEngine.Input.GetKeyDown(KeyCode.Escape);
            _inputData.NonGameplayInputData.PauseMenuReleased = UnityEngine.Input.GetKeyUp(KeyCode.Escape);
            _inputData.NonGameplayInputData.TildeClicked = UnityEngine.Input.GetKeyDown(KeyCode.Tilde);
            _inputData.NonGameplayInputData.TildeReleased = UnityEngine.Input.GetKeyUp(KeyCode.Tilde);
            _inputData.NonGameplayInputData.UpdateInputEvents();
        }

        private void UpdateUiInputData()
        {
            _inputData.UIInputData.InventoryClicked = UnityEngine.Input.GetKeyDown(KeyCode.Tab);
            _inputData.UIInputData.InventoryReleased = UnityEngine.Input.GetKeyUp(KeyCode.Tab);
            _inputData.UIInputData.UpdateInputEvents();
        }

        private void UpdateInteractionInputData()
        {
            _inputData.InteractionInputData.InteractedClicked = UnityEngine.Input.GetKeyDown(KeyCode.E);
            _inputData.InteractionInputData.InteractedReleased = UnityEngine.Input.GetKeyUp(KeyCode.E);
        }

        private void UpdateCameraInput()
        {
            _inputData.CameraInputData.InputVectorX = UnityEngine.Input.GetAxis("Mouse X");
            _inputData.CameraInputData.InputVectorY = UnityEngine.Input.GetAxis("Mouse Y");

            _inputData.CameraInputData.ZoomClicked = UnityEngine.Input.GetMouseButtonDown(2);
            _inputData.CameraInputData.ZoomReleased = UnityEngine.Input.GetMouseButtonUp(2);
        }

        private void UpdateMovementInputData()
        {
            _inputData.MovementInputData.InputVectorX = UnityEngine.Input.GetAxisRaw("Horizontal");
            _inputData.MovementInputData.InputVectorY = UnityEngine.Input.GetAxisRaw("Vertical");

            _inputData.MovementInputData.RunClicked = UnityEngine.Input.GetKeyDown(KeyCode.LeftShift);
            _inputData.MovementInputData.RunReleased = UnityEngine.Input.GetKeyUp(KeyCode.LeftShift);

            if ( _inputData.MovementInputData.RunClicked)
                _inputData.MovementInputData.IsRunning = true;

            if ( _inputData.MovementInputData.RunReleased)
                _inputData.MovementInputData.IsRunning = false;

            _inputData.MovementInputData.JumpClicked = UnityEngine.Input.GetKeyDown(KeyCode.Space);
            _inputData.MovementInputData.CrouchClicked = UnityEngine.Input.GetKeyDown(KeyCode.LeftControl);
        }

        private void UpdateEquipmentInputData()
        {
            _inputData.EquipmentInputData.FlashlightClicked = UnityEngine.Input.GetKey(KeyCode.F);//getKey allow read held key
            _inputData.EquipmentInputData.ScrollWheelDirection = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
            _inputData.EquipmentInputData.FlashlightReleased = UnityEngine.Input.GetKeyUp(KeyCode.F);
            _inputData.EquipmentInputData.UsingClicked = UnityEngine.Input.GetMouseButtonDown(0);
            _inputData.EquipmentInputData.UsingReleased = UnityEngine.Input.GetMouseButtonUp(0);
            _inputData.EquipmentInputData.SwitchWeaponPressed = UnityEngine.Input.GetKeyDown(KeyCode.Q);
            _inputData.EquipmentInputData.SwitchWeaponReleased = UnityEngine.Input.GetKeyUp(KeyCode.Q);
            _inputData.EquipmentInputData.TurnOnClicked = UnityEngine.Input.GetMouseButtonUp(1);
            _inputData.EquipmentInputData.TurnOnReleased = UnityEngine.Input.GetMouseButtonUp(1);
            _inputData.EquipmentInputData.VisorUpClicked = UnityEngine.Input.GetKeyDown(KeyCode.V);
            _inputData.EquipmentInputData.VisorUpReleased = UnityEngine.Input.GetKeyUp(KeyCode.V);
            _inputData.EquipmentInputData.AirBypassClicked = UnityEngine.Input.GetKeyDown(KeyCode.B);
            _inputData.EquipmentInputData.AirBypassReleased = UnityEngine.Input.GetKeyUp(KeyCode.B);

            _inputData.EquipmentInputData.FirstEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha1);
            _inputData.EquipmentInputData.FirstEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha1);
            _inputData.EquipmentInputData.SecondEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha2);
            _inputData.EquipmentInputData.SecondEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha2);
            _inputData.EquipmentInputData.ThirdEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha3);
            _inputData.EquipmentInputData.ThirdEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha3);
            _inputData.EquipmentInputData.FourthEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha4);
            _inputData.EquipmentInputData.FourthEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha4);
            _inputData.EquipmentInputData.UpdateInputEvents();
        }

        public void ResetInput()
        {
            _inputData.NonGameplayInputData.ResetInput();
            _inputData.UIInputData.ResetInput();
            _inputData.CameraInputData.ResetInput();
            _inputData.MovementInputData.ResetInput();
            _inputData.InteractionInputData.ResetInput();
            _inputData.EquipmentInputData.ResetInput();
        }

        public void SetPaused(bool isPaused)
        {
            SetInputActive(!isPaused);
        }
    }
}