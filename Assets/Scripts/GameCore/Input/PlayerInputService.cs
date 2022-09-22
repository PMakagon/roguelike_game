using System;
using LiftGame.GameCore.Input.Data;
using UnityEngine;

namespace LiftGame.GameCore.Input
{
    public class PlayerInputService : IPlayerInputService
    {
        private InputDataProvider _inputData;
        private bool _isInputActive;

        public void Initialize(InputDataProvider inputDataProvider)
        {
            _inputData = inputDataProvider;
            ResetInput();
        }
        
        public void UpdateInput()
        {
            GetNonGameplayInputData();
            if (!_isInputActive) return;
            GetUiInputData();
            GetCameraInput();
            GetMovementInputData();
            GetInteractionInputData();
            GetEquipmentInputData();
        }

        public void SetInputActive(bool state)
        {
            if (state==false) ResetInput();
            _isInputActive = state;
        }

        private void GetNonGameplayInputData()
        {
            _inputData.NonGameplayInputData.PauseMenuClicked = UnityEngine.Input.GetKeyDown(KeyCode.Escape);
            _inputData.NonGameplayInputData.PauseMenuReleased = UnityEngine.Input.GetKeyUp(KeyCode.Escape);
            _inputData.NonGameplayInputData.TildeClicked = UnityEngine.Input.GetKeyDown(KeyCode.Tilde);
            _inputData.NonGameplayInputData.TildeReleased = UnityEngine.Input.GetKeyUp(KeyCode.Tilde);
            _inputData.NonGameplayInputData.UpdateInputEvents();
        }

        private void GetUiInputData()
        {
            _inputData.UIInputData.InventoryClicked = UnityEngine.Input.GetKeyDown(KeyCode.Tab);
            _inputData.UIInputData.InventoryReleased = UnityEngine.Input.GetKeyUp(KeyCode.Tab);
            _inputData.UIInputData.UpdateInputEvents();
        }

        private void GetInteractionInputData()
        {
            _inputData.InteractionInputData.InteractedClicked = UnityEngine.Input.GetKeyDown(KeyCode.E);
            _inputData.InteractionInputData.InteractedReleased = UnityEngine.Input.GetKeyUp(KeyCode.E);
        }

        private void GetCameraInput()
        {
            _inputData.CameraInputData.InputVectorX = UnityEngine.Input.GetAxis("Mouse X");
            _inputData.CameraInputData.InputVectorY = UnityEngine.Input.GetAxis("Mouse Y");

            _inputData.CameraInputData.ZoomClicked = UnityEngine.Input.GetMouseButtonDown(2);
            _inputData.CameraInputData.ZoomReleased = UnityEngine.Input.GetMouseButtonUp(2);
        }

        private void GetMovementInputData()
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

        private void GetEquipmentInputData()
        {
            _inputData.EquipmentInputData.FlashlightClicked = UnityEngine.Input.GetKeyDown(KeyCode.F);
            _inputData.EquipmentInputData.FlashlightReleased = UnityEngine.Input.GetKeyUp(KeyCode.F);
            _inputData.EquipmentInputData.UsingClicked = UnityEngine.Input.GetMouseButtonDown(0);
            _inputData.EquipmentInputData.UsingReleased = UnityEngine.Input.GetMouseButtonUp(0);
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