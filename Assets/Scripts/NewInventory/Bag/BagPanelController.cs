using System;
using FarrokhGames.Inventory;
using LiftGame.NewInventory.Case;
using LiftGame.PlayerCore;
using UnityEngine;
using Zenject;

namespace LiftGame.NewInventory.Bag
{
    public class BagPanelController : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer renderer;
        [SerializeField] private GameObject bagPanel;
        private BagItemProvider _provider;
        private IPlayerInventoryService _inventoryService;

        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        private void Start()
        {
            _inventoryService.onInventoryLoad += Init;
        }
        

        private void Init()
        {
            if (_inventoryService.GetBag() == null) throw new NullReferenceException("Provider not exist");
            _provider = _inventoryService.GetBag();
            var inventoryManager = new InventoryManager(_provider,3,3);
            renderer.SetInventory(inventoryManager,_provider.InventoryRenderMode);
        }
    }
}