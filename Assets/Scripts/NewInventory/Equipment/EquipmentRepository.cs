using FarrokGames.Inventory.Runtime;
using FarrokhGames.Inventory;
using LiftGame.NewInventory.Items;
using UnityEngine;

namespace LiftGame.NewInventory.Equipment
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private IInventoryItem _inventoryItem;
        private readonly int _slotId;
        public readonly InventoryRenderMode InventoryRenderMode = InventoryRenderMode.Single;

        public EquipmentRepository(int slotId)
        {
            _slotId = slotId;
        }

        public bool IsEmpty => _inventoryItem == null;
        public bool IsSelected { get; set; }
        public int SlotId => _slotId;

        public IInventoryItem GetInventoryItem() => _inventoryItem;
        public EquipmentItem GetEquipmentItem() => _inventoryItem as EquipmentItem;

        public bool CanAddEquipmentItem(IInventoryItem item)
        {
            return ((ItemDefinition)item).ItemType == ItemType.Equipment;
        }

        public bool CanRemoveEquipmentItem()
        {
            return true;
        }

        public bool CanDropEquipmentItem()
        {
            return true;
        }

        public bool AddEquipmentItem(IInventoryItem item)
        {
            if (!IsEmpty) return false;
            if (!CanAddEquipmentItem(item)) return false;
            _inventoryItem = item;
            return true;
        }

        public bool RemoveEquipmentItem()
        {
            _inventoryItem = null;
            return true;
        }

        public bool DropEquipmentItem()
        {
            return RemoveEquipmentItem();
        }
    }
}