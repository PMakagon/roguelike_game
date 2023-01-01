using LiftGame.FPSController.InteractionSystem;
using LiftGame.GameCore.Input.Data;
using LiftGame.NewInventory.Container;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.NewInventory
{
    public class InteractableContainer : Interactable
    {
        [SerializeField] private ContainerConfig containerConfig;
        [SerializeField] private Animator containerAnimator;
        private ContainerItemRepository _containerRepository;
        public bool isExamined = false;
        private static readonly int Open = Animator.StringToHash("Open");

        private void Awake()
        {
            TooltipMessage = "Open " + containerConfig.ContainerName;
        }

        private void OnDestroy()
        {
            _containerRepository = null;
        }

        private void CloseContainer()
        {
            if (containerAnimator) containerAnimator.SetBool(Open,false);
            UIInputData.OnInventoryClicked -= CloseContainer;
        }

        public override void OnInteract(IPlayerData playerData)
        {
            UIInputData.OnInventoryClicked += CloseContainer;
            if (containerAnimator) containerAnimator.SetBool(Open,true);
            if (!isExamined)
            {
                _containerRepository = new ContainerItemRepository(containerConfig);
                isExamined = true;
            }
            var inventoryData = playerData.GetInventoryData();
            inventoryData.CurrentContainer = _containerRepository;
            inventoryData.OnWorldContainerOpen?.Invoke();
        }
    }
    
}