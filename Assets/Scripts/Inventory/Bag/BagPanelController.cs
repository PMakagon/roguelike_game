using System;
using LiftGame.Inventory.Core;
using UnityEngine;
using Zenject;

namespace LiftGame.Inventory.Bag
{
    public class BagPanelController : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer inventoryRenderer;
        [SerializeField] private GameObject bagPanel;
        private BagItemRepository _repository;
        private IPlayerInventoryService _inventoryService;

        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        private void Start()
        {
            _inventoryService.OnInventoryLoad += Init;
        }
        

        private void Init()
        {
            _repository = _inventoryService.GetBagRepository();
            if (_repository == null) throw new NullReferenceException("Provider not found");
            inventoryRenderer.SetInventory(_inventoryService.GetBagRepositoryManager(),_repository.InventoryRenderMode);
        }
    }
}