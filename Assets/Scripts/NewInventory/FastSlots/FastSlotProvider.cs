using System.Collections.Generic;
using FarrokhGames.Inventory;
using LiftGame.NewInventory.Items;

namespace LiftGame.NewInventory.FastSlots
{
    public class FastSlotProvider : IInventoryProvider
    {
        private IInventoryItem[] _items = new IInventoryItem[5];
        
        public int InventoryItemCount => _items.Length;

        public InventoryRenderMode InventoryRenderMode => InventoryRenderMode.Grid;

        public bool IsInventoryFull
        {
            get
            {
                for (int i = 0; i < _items.Length; i++)
                {
                    if (_items[i]==null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool AddInventoryItem(IInventoryItem item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (item == _items[i])
                {
                    return false;
                }

                if (_items[i]==null)
                {
                    _items[i] = item;
                    return true;
                }
            }
            return false;
        }

        public bool DropInventoryItem(IInventoryItem item)
        {
            return RemoveInventoryItem(item);
        }

        public IInventoryItem GetInventoryItem(int index)
        {
            return _items[index];
        }

        public List<IInventoryItem> GetAllItems()
        {
            return new List<IInventoryItem>(_items);
        }

        public bool CanAddInventoryItem(IInventoryItem item)
        {
            return ((ItemDefinition)item).ItemType == ItemType.Consumable;
        }

        public bool CanRemoveInventoryItem(IInventoryItem item)
        {
            return true;
        }

        public bool CanDropInventoryItem(IInventoryItem item)
        {
            return true;
        }

        public bool RemoveInventoryItem(IInventoryItem item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (item == _items[i])
                {
                    _items[i] = null;
                    return true;
                }
            }
            return false;
        }
        
    }
}