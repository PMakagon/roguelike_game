using FarrokhGames.Inventory;
using LiftGame.NewInventory.Items;

namespace LiftGame.NewInventory.Equipment
{
    public class EquipmentSlotProvider : IEquipmentSlotProvider
    {
        private IInventoryItem _equipment;

        public InventoryRenderMode InventoryRenderMode { get; }
        public bool IsEmpty { get; }
        
        public IInventoryItem GetEquipmentItem() => _equipment;

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
            _equipment = item;
            return true;
        }

        public bool RemoveEquipmentItem()
        {
            _equipment = null;
            return true;
        }

        public bool DropEquipmentItem()
        {
            return RemoveEquipmentItem();
        }
    }
}