using System;
using LiftGame.Inventory.Core;
using LiftGame.Ui;
using ModestTree;
using UnityEngine;
using Zenject;

namespace LiftGame.Inventory.PowerCellSlots
{
    public class PowerCellSlotsController : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer firstSlotRenderer;
        [SerializeField] private PowerSlotCapacityBar firstSlotBar;
        [SerializeField] private InventoryRenderer secondSlotRenderer;
        [SerializeField] private PowerSlotCapacityBar secondSlotBar;
        [SerializeField] private InventoryRenderer thirdSlotRenderer;
        [SerializeField] private PowerSlotCapacityBar thirdSlotBar;
        private IPlayerInventoryService _inventoryService;
        
        //MonoBehaviour injection
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
            firstSlotRenderer.SetupRenderer();
            secondSlotRenderer.SetupRenderer();
            thirdSlotRenderer.SetupRenderer();
            SetInventoryToRenderers();
            SetCapacityBars();
        }

        private void SetCapacityBars()
        {
            firstSlotBar.SetupPowerCellCapacityBar(_inventoryService.GetPowerCellSlotRepositoryManager(0));
            secondSlotBar.SetupPowerCellCapacityBar(_inventoryService.GetPowerCellSlotRepositoryManager(1));
            thirdSlotBar.SetupPowerCellCapacityBar(_inventoryService.GetPowerCellSlotRepositoryManager(2));
        }

        private void SetInventoryToRenderers()
        {
            firstSlotRenderer.RenderInventory(_inventoryService.GetPowerCellSlotRepositoryManager(0),InventoryRenderMode.Single);
            secondSlotRenderer.RenderInventory(_inventoryService.GetPowerCellSlotRepositoryManager(1),InventoryRenderMode.Single);
            thirdSlotRenderer.RenderInventory(_inventoryService.GetPowerCellSlotRepositoryManager(2),InventoryRenderMode.Single);
        }
    }
}