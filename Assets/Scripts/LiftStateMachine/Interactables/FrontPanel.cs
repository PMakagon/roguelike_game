using System;
using FPSController;
using FPSController.Interaction_System;
using InventorySystem;
using UnityEngine;
using UnityEngine.Events;
using VHS;

namespace LiftStateMachine
{
    public class FrontPanel : Interactable
    {
        [SerializeField] private Light buttonLight;
        [SerializeField] private LiftControllerData liftControllerData;
        [SerializeField] private int thisFloorNumber;
        


        private void Update()
        {
            if ( liftControllerData.CurrentFloor == thisFloorNumber)
            {
                buttonLight.enabled = false;
                liftControllerData.IsLiftCalled = false;
            }
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            // buttonAnimation.Play();
            if (liftControllerData.CurrentFloor != thisFloorNumber)
            { 
                buttonLight.enabled = true;
                liftControllerData.DestinationFloor = thisFloorNumber;
                liftControllerData.IsLiftCalled = true;
            }
        }

        
    }
}