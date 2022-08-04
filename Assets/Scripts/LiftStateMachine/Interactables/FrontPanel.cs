using System;
using FPSController.Interaction_System;
using InventorySystem;
using UnityEngine;

namespace LiftStateMachine.Interactables
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
            // if (inventoryData.Items.Contains())
            // {
            //     
            // }
            liftControllerData.IsLiftCalled = true;
            LiftControllerData.OnLiftCalled.Invoke();
        }
    }
}