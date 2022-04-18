using System;
using FPSController;
using UnityEngine;
using UnityEngine.Events;

namespace LiftStateMachine
{
    public class FrontPanel : InteractableBase
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

        public override void OnInteract()
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