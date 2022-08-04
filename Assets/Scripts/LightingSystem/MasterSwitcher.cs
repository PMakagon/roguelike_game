using FPSController;
using FPSController.Interaction_System;
using InventorySystem;
using UnityEngine;

namespace LightingSystem
{
    public class MasterSwitcher : Interactable
    {
        [SerializeField] private bool isSwitchedOn;
        [SerializeField] private Transform button;
        
        
        public bool IsSwitchedOn
        {
            get => isSwitchedOn;
            set => isSwitchedOn = value;
        }


        public override void OnInteract(InventoryData inventoryData)
        {
            isSwitchedOn = !isSwitchedOn;
            if (isSwitchedOn)
            {
                button.Rotate(0.0f, 0.0f, +100.0f, Space.Self);
            }
            else
            {
                button.Rotate(0.0f, 0.0f, -100.0f, Space.Self);
            }
        }
    }
}
