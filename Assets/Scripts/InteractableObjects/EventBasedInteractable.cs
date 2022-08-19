using LiftGame.FPSController.InteractionSystem;
using LiftGame.InventorySystem;
using UnityEngine;
using UnityEngine.Events;

namespace LiftGame.InteractableObjects
{
    public class EventBasedInteractable : Interactable
    {
        [SerializeField] private UnityEvent onInteracted;
        
        public override void OnInteract(InventoryData inventoryData)
        {
            onInteracted?.Invoke();
        }
    }
}