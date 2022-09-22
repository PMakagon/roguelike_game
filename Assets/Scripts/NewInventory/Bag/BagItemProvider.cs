using System.Collections.Generic;
using FarrokhGames.Inventory;

namespace LiftGame.NewInventory.Bag
{
    public class BagItemProvider : IInventoryProvider
    {
        private readonly List<IInventoryItem> _items;
        private readonly int _maximumItemCount;

        public BagItemProvider(int widht, int height)
        {
            _maximumItemCount = height*widht;
            _items = new List<IInventoryItem>(_maximumItemCount);
        }
        
        public int InventoryItemCount => _items.Count;

        public InventoryRenderMode InventoryRenderMode => InventoryRenderMode.Grid;

        public bool IsInventoryFull
        {
            get
            {
                if (_maximumItemCount < 0)return false;
                return InventoryItemCount >= _maximumItemCount;
            }
        }

        public bool AddInventoryItem(IInventoryItem item)
        {
            if (IsInventoryFull) return false;
            if (!_items.Contains(item))
            {
                _items.Add(item);
                return true;
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
            return _items;
        }

        public bool CanAddInventoryItem(IInventoryItem item)
        {
            return true;
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
            return _items.Remove(item);
        }
    }
}