using LiftGame.Inventory.Core;
using LiftGame.Inventory.Items;

namespace LiftGame.Inventory.PowerCellSlots
{
    public interface IPowerCellSlotRepository
    {
        public bool IsEmpty { get; }
        public int SlotId { get; }
        public bool IsLocked { get; set; }
        
        public IInventoryItem GetInventoryItem();
        
        public PowerCell GetPowerCellItem();
        
        public bool CanAddCellItem(IInventoryItem item);
        
        public bool CanRemoveCellItem();
        
        public bool CanDropCellItem();

        public bool AddCellItem(IInventoryItem item);

        public bool RemoveCellItem();

        public bool DropCellItem();
    }
}