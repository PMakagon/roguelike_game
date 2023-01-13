using LiftGame.FPSController.InteractionSystem;
using UnityEngine;
using UnityEngine.Events;

namespace LiftGame.InteractableObjects
{
    public class EventBasedInteractable : Interactable
    {
        [SerializeField] private UnityEvent onInteracted;
        private Interaction _toDoSmth;

        public override string TooltipMessage => gameObject.name;

        public override void CreateInteractions()
        {
            _toDoSmth = new Interaction("Interact", true);
        }

        public override void BindInteractions()
        {
            _toDoSmth.actionOnInteract = InvokeEvents;
        }
        
        public override void AddInteractions()
        {
            Interactions.Add(_toDoSmth);
        }

        private bool InvokeEvents()
        {
            onInteracted?.Invoke();
            return true;
        }
        
    }
}