using UnityEngine;

namespace LiftGame.GameCore.Input.Data
{
    [CreateAssetMenu(fileName = "InputDataProvider", menuName = "PlayerInputData/InputDataProvider", order = 1)]
    public class InputDataProvider : ScriptableObject
    {
        [Space, Header("Input Data")] 
        [SerializeField] private CameraInputData cameraInputData = null;
        [SerializeField] private MovementInputData movementInputData = null;
        [SerializeField] private InteractionInputData interactionInputData = null;
        [SerializeField] private EquipmentInputData equipmentInputData = null;
        [SerializeField] private UIInputData uiInputData = null;
        [SerializeField] private NonGameplayInputData nonGameplayInputData = null;

        public CameraInputData CameraInputData => cameraInputData;

        public MovementInputData MovementInputData => movementInputData;

        public InteractionInputData InteractionInputData => interactionInputData;

        public EquipmentInputData EquipmentInputData => equipmentInputData;

        public UIInputData UIInputData => uiInputData;

        public NonGameplayInputData NonGameplayInputData => nonGameplayInputData;
    }
}