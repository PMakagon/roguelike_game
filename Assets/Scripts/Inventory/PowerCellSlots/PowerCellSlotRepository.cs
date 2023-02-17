using LiftGame.Inventory.Core;
using LiftGame.Inventory.Items;

namespace LiftGame.Inventory.PowerCellSlots
{
    public class PowerCellSlotRepository : IPowerCellSlotRepository
    {
        private IInventoryItem _inventoryItem;
        private readonly int _slotId;
        public readonly InventoryRenderMode InventoryRenderMode = InventoryRenderMode.Single;

        public PowerCellSlotRepository(int slotId)
        {
            _slotId = slotId;
        }

        public bool IsEmpty => _inventoryItem == null;
        
        public bool IsLocked { get; set; }
        public int SlotId => _slotId;
        
        public IInventoryItem GetInventoryItem() => _inventoryItem;
        
        public PowerCell GetPowerCellItem() => _inventoryItem as PowerCell;

        public bool CanAddCellItem(IInventoryItem item)
        {
            return ((ItemDefinition)item).ItemType == ItemType.PowerCell;
        }

        public bool CanRemoveCellItem()
        {
            return true;
        }

        public bool CanDropCellItem()
        {
            return true;
        }

        public bool AddCellItem(IInventoryItem item)
        {
            if (!IsEmpty) return false;
            if (!CanAddCellItem(item)) return false;
            _inventoryItem = item;
            return true;
        }

        public bool RemoveCellItem()
        {
            _inventoryItem = null;
            return true;
        }

        public bool DropCellItem()
        {
            return RemoveCellItem();
        }
    }
}