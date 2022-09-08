
using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.Pause;
using UnityEngine;

namespace LiftGame.GameCore.Input
{
    /// <summary>
    /// for simple input handling
    /// </summary>
    public class MonoInputHandler : MonoBehaviour,IPauseable
    {
        [SerializeField] private InputData inputData;
        private bool _isInputActive;

        void Start()
        {
            ResetInput();
        }

        void Update()
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
            _isInputActive = state;
        }

        private void GetNonGameplayInputData()
        {
            inputData.NonGameplayInputData.PauseMenuClicked = UnityEngine.Input.GetKeyDown(KeyCode.Escape);
            inputData.NonGameplayInputData.PauseMenuReleased = UnityEngine.Input.GetKeyUp(KeyCode.Escape);
            inputData.NonGameplayInputData.TildeClicked = UnityEngine.Input.GetKeyDown(KeyCode.Tilde);
            inputData.NonGameplayInputData.TildeReleased = UnityEngine.Input.GetKeyUp(KeyCode.Tilde);
        }

        private void GetUiInputData()
        {
            inputData.UIInputData.InventoryClicked = UnityEngine.Input.GetKeyDown(KeyCode.Tab);
            inputData.UIInputData.InventoryReleased = UnityEngine.Input.GetKeyUp(KeyCode.Tab);
        }

        private void GetInteractionInputData()
        {
            inputData.InteractionInputData.InteractedClicked = UnityEngine.Input.GetKeyDown(KeyCode.E);
            inputData.InteractionInputData.InteractedReleased = UnityEngine.Input.GetKeyUp(KeyCode.E);
        }

        private void GetCameraInput()
        {
            inputData.CameraInputData.InputVectorX = UnityEngine.Input.GetAxis("Mouse X");
            inputData.CameraInputData.InputVectorY = UnityEngine.Input.GetAxis("Mouse Y");

            inputData.CameraInputData.ZoomClicked = UnityEngine.Input.GetMouseButtonDown(2);
            inputData.CameraInputData.ZoomReleased = UnityEngine.Input.GetMouseButtonUp(2);
        }

        private void GetMovementInputData()
        {
            inputData.MovementInputData.InputVectorX = UnityEngine.Input.GetAxisRaw("Horizontal");
            inputData.MovementInputData.InputVectorY = UnityEngine.Input.GetAxisRaw("Vertical");

            inputData.MovementInputData.RunClicked = UnityEngine.Input.GetKeyDown(KeyCode.LeftShift);
            inputData.MovementInputData.RunReleased = UnityEngine.Input.GetKeyUp(KeyCode.LeftShift);

            if ( inputData.MovementInputData.RunClicked)
                inputData.MovementInputData.IsRunning = true;

            if ( inputData.MovementInputData.RunReleased)
                inputData.MovementInputData.IsRunning = false;

            inputData.MovementInputData.JumpClicked = UnityEngine.Input.GetKeyDown(KeyCode.Space);
            inputData.MovementInputData.CrouchClicked = UnityEngine.Input.GetKeyDown(KeyCode.LeftControl);
        }

        private void GetEquipmentInputData()
        {
            inputData.EquipmentInputData.FlashlightClicked = UnityEngine.Input.GetKeyDown(KeyCode.F);
            inputData.EquipmentInputData.FlashlightReleased = UnityEngine.Input.GetKeyUp(KeyCode.F);
            inputData.EquipmentInputData.UsingClicked = UnityEngine.Input.GetMouseButtonDown(0);
            inputData.EquipmentInputData.UsingReleased = UnityEngine.Input.GetMouseButtonUp(0);
            inputData.EquipmentInputData.TurnOnClicked = UnityEngine.Input.GetMouseButtonUp(1);
            inputData.EquipmentInputData.TurnOnReleased = UnityEngine.Input.GetMouseButtonUp(1);
            inputData.EquipmentInputData.VisorUpClicked = UnityEngine.Input.GetKeyDown(KeyCode.V);
            inputData.EquipmentInputData.VisorUpReleased = UnityEngine.Input.GetKeyUp(KeyCode.V);
            inputData.EquipmentInputData.AirBypassClicked = UnityEngine.Input.GetKeyDown(KeyCode.B);
            inputData.EquipmentInputData.AirBypassReleased = UnityEngine.Input.GetKeyUp(KeyCode.B);

            inputData.EquipmentInputData.FirstEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha1);
            inputData.EquipmentInputData.FirstEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha1);
            inputData.EquipmentInputData.SecondEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha2);
            inputData.EquipmentInputData.SecondEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha2);
            inputData.EquipmentInputData.ThirdEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha3);
            inputData.EquipmentInputData.ThirdEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha3);
            inputData.EquipmentInputData.FourthEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha4);
            inputData.EquipmentInputData.FourthEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha4);
        }

        public void ResetInput()
        {
            inputData.NonGameplayInputData.ResetInput();
            inputData.UIInputData.ResetInput();
            inputData.CameraInputData.ResetInput();
            inputData.MovementInputData.ResetInput();
            inputData.InteractionInputData.ResetInput();
            inputData.EquipmentInputData.ResetInput();
        }

        public void SetPaused(bool isPaused)
        {
            _isInputActive = !isPaused;
        }
    }
}