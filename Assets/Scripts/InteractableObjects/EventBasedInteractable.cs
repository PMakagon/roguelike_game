using FPSController.Interaction_System;
using InventorySystem;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableObjects
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