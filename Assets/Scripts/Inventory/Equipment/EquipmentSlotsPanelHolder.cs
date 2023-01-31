using System;
using LiftGame.Inventory.Core;
using UnityEngine;
using Zenject;

namespace LiftGame.Inventory.Equipment
{
    public class EquipmentSlotsPanelHolder : MonoBehaviour
    {
        [SerializeField] private InventoryRenderer leftSlotRenderer;
        [SerializeField] private InventoryRenderer rightSlotRenderer;
        private IPlayerInventoryService _inventoryService;
        
        //MonoBehaviour injection
        [Inject]
        private void Construct(IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        private void Awake()
        {
            if (leftSlotRenderer == null || rightSlotRenderer== null )
            {
                Debug.LogError("Equipment Slot Renderer not found");
            }
            _inventoryService.OnInventoryLoad += Init;
        }
        

        private void OnDestroy()
        {
            _inventoryService.OnInventoryLoad -= Init;
        }

        private void Init()
        {
            leftSlotRenderer.SetupRenderer();
            rightSlotRenderer.SetupRenderer();
            SetInventoryToRenderers();
        }

        private void SetInventoryToRenderers()
        {
            leftSlotRenderer.RenderInventory(_inventoryService.GetEquipmentRepositoryManager(0),InventoryRenderMode.Single);
            rightSlotRenderer.RenderInventory(_inventoryService.GetEquipmentRepositoryManager(1),InventoryRenderMode.Single);
        }
        
    }
}