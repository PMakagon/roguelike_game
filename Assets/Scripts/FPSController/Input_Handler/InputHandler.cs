using FPSController.Scriptable_Objects;
using PlayerEquipment;
using UnityEngine;

namespace FPSController.Input_Handler
{
    public class InputHandler : MonoBehaviour
    {
        #region Data

        [Space, Header("Input Data")] [SerializeField]
        private CameraInputData cameraInputData = null;

        [SerializeField] private MovementInputData movementInputData = null;
        [SerializeField] private InteractionInputData interactionInputData = null;
        [SerializeField] private EquipmentInputData equipmentInputData = null;

        #endregion

        #region BuiltIn Methods

        void Start()
        {
            cameraInputData.ResetInput();
            movementInputData.ResetInput();
            interactionInputData.ResetInput();
            equipmentInputData.ResetInput();
        }

        void Update()
        {
            GetCameraInput();
            GetMovementInputData();
            GetInteractionInputData();
            GetEquipmentInputData();
        }

        #endregion

        #region Custom Methods

        private void GetInteractionInputData()
        {
            interactionInputData.InteractedClicked = Input.GetKeyDown(KeyCode.E);
            interactionInputData.InteractedReleased = Input.GetKeyUp(KeyCode.E);
        }

        private void GetCameraInput()
        {
            cameraInputData.InputVectorX = Input.GetAxis("Mouse X");
            cameraInputData.InputVectorY = Input.GetAxis("Mouse Y");

            cameraInputData.ZoomClicked = Input.GetMouseButtonDown(2);
            cameraInputData.ZoomReleased = Input.GetMouseButtonUp(2);
        }

        private void GetMovementInputData()
        {
            movementInputData.InputVectorX = Input.GetAxisRaw("Horizontal");
            movementInputData.InputVectorY = Input.GetAxisRaw("Vertical");

            movementInputData.RunClicked = Input.GetKeyDown(KeyCode.LeftShift);
            movementInputData.RunReleased = Input.GetKeyUp(KeyCode.LeftShift);

            if (movementInputData.RunClicked)
                movementInputData.IsRunning = true;

            if (movementInputData.RunReleased)
                movementInputData.IsRunning = false;

            movementInputData.JumpClicked = Input.GetKeyDown(KeyCode.Space);
            movementInputData.CrouchClicked = Input.GetKeyDown(KeyCode.LeftControl);
        }

        private void GetEquipmentInputData()
        {
            equipmentInputData.FlashlightClicked = Input.GetKeyDown(KeyCode.F);
            equipmentInputData.FlashlightReleased = Input.GetKeyUp(KeyCode.F);
            equipmentInputData.UsingClicked = Input.GetMouseButtonDown(0);
            equipmentInputData.UsingReleased = Input.GetMouseButtonUp(0);
            equipmentInputData.TurnOnClicked = Input.GetMouseButtonUp(1);
            equipmentInputData.TurnOnReleased = Input.GetMouseButtonUp(1);
            equipmentInputData.VisorUpClicked = Input.GetKeyDown(KeyCode.V);
            equipmentInputData.VisorUpReleased = Input.GetKeyUp(KeyCode.V);
            equipmentInputData.AirBypassClicked = Input.GetKeyDown(KeyCode.B);
            equipmentInputData.AirBypassReleased = Input.GetKeyUp(KeyCode.B);

            equipmentInputData.FirstEquipmentClicked = Input.GetKeyDown(KeyCode.Alpha1);
            equipmentInputData.FirstEquipmentReleased = Input.GetKeyUp(KeyCode.Alpha1);
            equipmentInputData.SecondEquipmentClicked = Input.GetKeyDown(KeyCode.Alpha2);
            equipmentInputData.SecondEquipmentReleased = Input.GetKeyUp(KeyCode.Alpha2);
            equipmentInputData.ThirdEquipmentClicked = Input.GetKeyDown(KeyCode.Alpha3);
            equipmentInputData.ThirdEquipmentReleased = Input.GetKeyUp(KeyCode.Alpha3);
            equipmentInputData.FourthEquipmentClicked = Input.GetKeyDown(KeyCode.Alpha4);
            equipmentInputData.FourthEquipmentReleased = Input.GetKeyUp(KeyCode.Alpha4);
            
        }
        

        #endregion
    }
}