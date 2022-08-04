using System.Collections.Generic;
using FPSController.Interaction_System;
using InventorySystem.Items;
using UnityEngine;

namespace InventorySystem
{
    public class InteractableContainer : Interactable
    {
        [SerializeField] private string containerName;
        private List<IItem> _containerItems;


        public override void OnInteract(InventoryData inventoryData)
        {
            inventoryData.ContainerName = containerName;
            // inventoryData.ContainerItems = _containerItems;
            inventoryData.ContainerItems = new List<IItem>(5);
            inventoryData.onContainerOpen.Invoke();
        }
    }
}