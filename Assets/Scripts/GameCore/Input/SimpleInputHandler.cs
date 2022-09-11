
using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.Pause;
using UnityEngine;

namespace LiftGame.GameCore.Input
{
    /// <summary>
    /// for fast scene setup
    /// </summary>
    public class SimpleInputHandler : MonoBehaviour,IPauseable
    {
        [SerializeField] private InputDataProvider inputDataProvider;
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
            inputDataProvider.NonGameplayInputData.PauseMenuClicked = UnityEngine.Input.GetKeyDown(KeyCode.Escape);
            inputDataProvider.NonGameplayInputData.PauseMenuReleased = UnityEngine.Input.GetKeyUp(KeyCode.Escape);
            inputDataProvider.NonGameplayInputData.TildeClicked = UnityEngine.Input.GetKeyDown(KeyCode.Tilde);
            inputDataProvider.NonGameplayInputData.TildeReleased = UnityEngine.Input.GetKeyUp(KeyCode.Tilde);
        }

        private void GetUiInputData()
        {
            inputDataProvider.UIInputData.InventoryClicked = UnityEngine.Input.GetKeyDown(KeyCode.Tab);
            inputDataProvider.UIInputData.InventoryReleased = UnityEngine.Input.GetKeyUp(KeyCode.Tab);
        }

        private void GetInteractionInputData()
        {
            inputDataProvider.InteractionInputData.InteractedClicked = UnityEngine.Input.GetKeyDown(KeyCode.E);
            inputDataProvider.InteractionInputData.InteractedReleased = UnityEngine.Input.GetKeyUp(KeyCode.E);
        }

        private void GetCameraInput()
        {
            inputDataProvider.CameraInputData.InputVectorX = UnityEngine.Input.GetAxis("Mouse X");
            inputDataProvider.CameraInputData.InputVectorY = UnityEngine.Input.GetAxis("Mouse Y");

            inputDataProvider.CameraInputData.ZoomClicked = UnityEngine.Input.GetMouseButtonDown(2);
            inputDataProvider.CameraInputData.ZoomReleased = UnityEngine.Input.GetMouseButtonUp(2);
        }

        private void GetMovementInputData()
        {
            inputDataProvider.MovementInputData.InputVectorX = UnityEngine.Input.GetAxisRaw("Horizontal");
            inputDataProvider.MovementInputData.InputVectorY = UnityEngine.Input.GetAxisRaw("Vertical");

            inputDataProvider.MovementInputData.RunClicked = UnityEngine.Input.GetKeyDown(KeyCode.LeftShift);
            inputDataProvider.MovementInputData.RunReleased = UnityEngine.Input.GetKeyUp(KeyCode.LeftShift);

            if ( inputDataProvider.MovementInputData.RunClicked)
                inputDataProvider.MovementInputData.IsRunning = true;

            if ( inputDataProvider.MovementInputData.RunReleased)
                inputDataProvider.MovementInputData.IsRunning = false;

            inputDataProvider.MovementInputData.JumpClicked = UnityEngine.Input.GetKeyDown(KeyCode.Space);
            inputDataProvider.MovementInputData.CrouchClicked = UnityEngine.Input.GetKeyDown(KeyCode.LeftControl);
        }

        private void GetEquipmentInputData()
        {
            inputDataProvider.EquipmentInputData.FlashlightClicked = UnityEngine.Input.GetKeyDown(KeyCode.F);
            inputDataProvider.EquipmentInputData.FlashlightReleased = UnityEngine.Input.GetKeyUp(KeyCode.F);
            inputDataProvider.EquipmentInputData.UsingClicked = UnityEngine.Input.GetMouseButtonDown(0);
            inputDataProvider.EquipmentInputData.UsingReleased = UnityEngine.Input.GetMouseButtonUp(0);
            inputDataProvider.EquipmentInputData.TurnOnClicked = UnityEngine.Input.GetMouseButtonUp(1);
            inputDataProvider.EquipmentInputData.TurnOnReleased = UnityEngine.Input.GetMouseButtonUp(1);
            inputDataProvider.EquipmentInputData.VisorUpClicked = UnityEngine.Input.GetKeyDown(KeyCode.V);
            inputDataProvider.EquipmentInputData.VisorUpReleased = UnityEngine.Input.GetKeyUp(KeyCode.V);
            inputDataProvider.EquipmentInputData.AirBypassClicked = UnityEngine.Input.GetKeyDown(KeyCode.B);
            inputDataProvider.EquipmentInputData.AirBypassReleased = UnityEngine.Input.GetKeyUp(KeyCode.B);

            inputDataProvider.EquipmentInputData.FirstEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha1);
            inputDataProvider.EquipmentInputData.FirstEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha1);
            inputDataProvider.EquipmentInputData.SecondEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha2);
            inputDataProvider.EquipmentInputData.SecondEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha2);
            inputDataProvider.EquipmentInputData.ThirdEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha3);
            inputDataProvider.EquipmentInputData.ThirdEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha3);
            inputDataProvider.EquipmentInputData.FourthEquipmentClicked = UnityEngine.Input.GetKeyDown(KeyCode.Alpha4);
            inputDataProvider.EquipmentInputData.FourthEquipmentReleased = UnityEngine.Input.GetKeyUp(KeyCode.Alpha4);
        }

        public void ResetInput()
        {
            inputDataProvider.NonGameplayInputData.ResetInput();
            inputDataProvider.UIInputData.ResetInput();
            inputDataProvider.CameraInputData.ResetInput();
            inputDataProvider.MovementInputData.ResetInput();
            inputDataProvider.InteractionInputData.ResetInput();
            inputDataProvider.EquipmentInputData.ResetInput();
        }

        public void SetPaused(bool isPaused)
        {
            _isInputActive = !isPaused;
        }
    }
}