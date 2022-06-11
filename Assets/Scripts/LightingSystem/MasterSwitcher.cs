using FPSController;
using FPSController.Interaction_System;
using InventorySystem;
using UnityEngine;

namespace LightingSystem
{
    public class MasterSwitcher : Interactable
    {
        [SerializeField] private bool isSwitchedOn;
        [SerializeField] private Animation switchAnimation;
    
    

        public bool IsSwitchedOn
        {
            get => isSwitchedOn;
            set => isSwitchedOn = value;
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            // switchAnimation.Play();
            isSwitchedOn = !isSwitchedOn;
        }
    }
}
