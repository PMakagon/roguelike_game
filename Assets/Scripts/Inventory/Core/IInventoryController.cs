using System;

namespace LiftGame.Inventory.Core
{
    public interface IInventoryController
    {
        Action<IInventoryItem> onItemHovered { get; set; }
        Action<IInventoryItem> onItemPickedUp { get; set; }
        Action<IInventoryItem> onItemAdded { get; set; }
        Action<IInventoryItem> onItemSwapped { get; set; }
        Action<IInventoryItem> onItemReturned { get; set; }
        Action<IInventoryItem> onItemDropped { get; set; }
    }
}