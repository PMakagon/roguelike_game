using LiftGame.Inventory.Core;
using LiftGame.Inventory.Items;

namespace LiftGame.Inventory.Equipment
{
    public interface IEquipmentRepository

    {
    // public InventoryRenderMode InventoryRenderMode { get; }

    // public int InventoryItemCount { get; }
    public bool IsEmpty { get; }
    public bool IsSelected { get; set; }
    public int SlotId { get; }
    public IInventoryItem GetInventoryItem();
    public EquipmentItem GetEquipmentItem();

    public bool CanAddEquipmentItem(IInventoryItem item);

    public bool CanRemoveEquipmentItem();

    public bool CanDropEquipmentItem();

    public bool AddEquipmentItem(IInventoryItem item);

    public bool RemoveEquipmentItem();

    public bool DropEquipmentItem();
    }
}