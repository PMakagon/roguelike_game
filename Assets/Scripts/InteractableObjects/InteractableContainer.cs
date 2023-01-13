using LiftGame.FPSController.InteractionSystem;
using LiftGame.GameCore.Input.Data;
using LiftGame.Inventory.Container;
using UnityEngine;

namespace LiftGame.InteractableObjects
{
    public class InteractableContainer : Interactable
    {
        [SerializeField] private ContainerConfig containerConfig;
        [SerializeField] private Animator containerAnimator;
        [SerializeField] private float holdDuration = 0f;

        private ContainerItemRepository _containerRepository;
        public bool isExamined = false;
        private static readonly int Open = Animator.StringToHash("Open");
        private Interaction _toOpen;

        private void Start()
        {
            TooltipMessage = containerConfig.ContainerName;
        }

        private void OnDestroy()
        {
            _containerRepository = null;
        }

        public override void CreateInteractions()
        {
            _toOpen = holdDuration == 0 ? new Interaction("Open", true) : new Interaction("Open", holdDuration, true);
        }

        public override void BindInteractions()
        {
            _toOpen.actionOnInteract = OpenContainer;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toOpen);
        }

        private void CloseContainer()
        {
            if (containerAnimator) containerAnimator.SetBool(Open, false);
            UIInputData.OnInventoryClicked -= CloseContainer;
        }

        private bool OpenContainer()
        {
            UIInputData.OnInventoryClicked += CloseContainer;
            if (containerAnimator) containerAnimator.SetBool(Open, true);
            if (!isExamined)
            {
                _containerRepository = new ContainerItemRepository(containerConfig);
                isExamined = true;
            }

            var inventoryData = CachedPlayerData.GetInventoryData();
            inventoryData.CurrentContainer = _containerRepository;
            inventoryData.OnWorldContainerOpen?.Invoke();
            return true;
        }
    }
}