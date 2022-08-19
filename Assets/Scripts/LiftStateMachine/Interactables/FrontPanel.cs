using LiftGame.FPSController.InteractionSystem;
using LiftGame.InventorySystem;
using UnityEngine;

namespace LiftGame.LiftStateMachine.Interactables
{
    public class FrontPanel : Interactable
    {
        [SerializeField] private Light buttonLight;
        [SerializeField] private LiftControllerData liftControllerData;
        
        private void Update()
        {
            buttonLight.enabled = liftControllerData.IsLiftCalled;
        }

        private void Awake()
        {
            buttonLight.enabled = false;
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            liftControllerData.IsLiftCalled = true;
            LiftControllerData.OnLiftCalled.Invoke();
        }
    }
}