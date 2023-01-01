using FarrokGames.Inventory.Runtime;
using FarrokhGames.Inventory;
using UnityEngine;
using Zenject;

namespace LiftGame.NewInventory.Equipment
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
        }

        private void Start()
        {
            _inventoryService.OnInventoryLoad += SetInventoryToRenderers;
        }

        private void SetInventoryToRenderers()
        {
            leftSlotRenderer.SetInventory(_inventoryService.GetEquipmentRepositoryManager(0),InventoryRenderMode.Single);
            rightSlotRenderer.SetInventory(_inventoryService.GetEquipmentRepositoryManager(1),InventoryRenderMode.Single);
        }
        
    }
}