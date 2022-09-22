using LiftGame.FPSController.InteractionSystem;
using LiftGame.NewInventory.Container;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.NewInventory
{
    public class InteractableContainer : Interactable
    {
        [SerializeField] private string containerName;
        [SerializeField] private ContainerConfig containerConfig;
        private ContainerItemProvider _containerProvider;
        public bool isExamined = false;


        public override void OnInteract(IPlayerData playerData)
        {
            if (!isExamined)
            {
                _containerProvider = new ContainerItemProvider(containerConfig);
                isExamined = true;
            }
            var inventoryData = playerData.GetInventoryData();
            inventoryData.CurrentContainer = _containerProvider;
            inventoryData.onContainerOpen.Invoke();
        }
    }
    
}