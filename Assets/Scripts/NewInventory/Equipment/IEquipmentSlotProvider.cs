using FarrokhGames.Inventory;

namespace LiftGame.NewInventory.Equipment
{
    public interface IEquipmentSlotProvider
    {
        public InventoryRenderMode InventoryRenderMode { get; }
        
        // public int InventoryItemCount { get; }
        public bool IsEmpty { get; }

        public IInventoryItem GetEquipmentItem();

        public bool CanAddEquipmentItem(IInventoryItem item);

        public bool CanRemoveEquipmentItem();

        public bool CanDropEquipmentItem();

        public bool AddEquipmentItem(IInventoryItem item);

        public bool RemoveEquipmentItem();

        public bool DropEquipmentItem();
    }
}