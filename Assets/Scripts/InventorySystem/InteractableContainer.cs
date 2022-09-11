using System.Collections.Generic;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.InventorySystem.Items;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.InventorySystem
{
    public class InteractableContainer : Interactable
    {
        [SerializeField] private string containerName;
        [SerializeField] private ItemContainer container;
        private List<IItem> _containerItems;


        public override void OnInteract(IPlayerData playerData)
        {
            var inventoryData = playerData.GetInventoryData();
            inventoryData.CurrentContainer = container;
            inventoryData.onContainerOpen.Invoke();
        }
    }
}