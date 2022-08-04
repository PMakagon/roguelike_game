using InventorySystem;
using UnityEngine;
using UnityEngine.Events;

namespace FPSController.Interaction_System
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