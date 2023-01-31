using System;
using LiftGame.Inventory.Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace LiftGame.Inventory.Bag
{
    public class BagPanelController : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer inventoryRenderer;
        [SerializeField] private TextMeshProUGUI bagLabel;
        private BagItemRepository _repository;
        private IPlayerInventoryService _inventoryService;

        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        private void Awake()
        {
            _inventoryService.OnInventoryLoad += Init;
        }

        private void OnDestroy()
        {
            _inventoryService.OnInventoryLoad -= Init;
        }


        private void Init()
        {
            inventoryRenderer.SetupRenderer();
            _repository = _inventoryService.GetBagRepository();
            bagLabel.text = _inventoryService.InventoryData.BagSlotConfig.ContainerName;
            if (_repository == null) throw new NullReferenceException("Provider not found");
            inventoryRenderer.RenderInventory(_inventoryService.GetBagRepositoryManager(),_repository.InventoryRenderMode);
        }
    }
}