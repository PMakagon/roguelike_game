using System;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.LiftStateMachine;
using UnityEngine;

namespace LiftGame.InteractableObjects.LiftInteractables
{
    public class FrontPanel : Interactable
    {
        [SerializeField] private Light buttonLight;
        [SerializeField] private LiftControllerData liftControllerData;
        private Interaction _toCall = new Interaction("Push",true);
        
        private void Update()
        {
            buttonLight.enabled = liftControllerData.IsLiftCalled;
        }

        private void Start()
        {
            buttonLight.enabled = false;
        }
        
        public override void BindInteractions()
        {
            _toCall.actionOnInteract = CallLift;
        }
        
        public override void AddInteractions()
        {
            Interactions.Add(_toCall);
        }
        

        private bool CallLift()
        {
            liftControllerData.IsLiftCalled = true;
            LiftControllerData.OnLiftCalled?.Invoke();
            return true;
        }
        
    }
}