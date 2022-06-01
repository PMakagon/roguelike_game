using FPSController;
using InventorySystem;
using UnityEngine;

namespace LightingSystem
{
    public class SlaveSwitcher : Interactable
    {
        [SerializeField] private bool isEnabled;
        
        public bool IsEnabled
        {
            get => isEnabled;
            set => isEnabled = value;
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            isEnabled = !isEnabled;
        }
    }
}