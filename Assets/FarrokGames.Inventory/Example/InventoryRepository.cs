using System.Collections.Generic;
using FarrokGames.Inventory.Runtime;

namespace FarrokhGames.Inventory.Examples
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<IInventoryItem> _items;
        private int _maximumAlowedItemCount;

        /// <summary>
        /// CTOR
        /// </summary>
        public InventoryRepository(InventoryRenderMode renderMode, int maximumAlowedItemCount)
        {
            InventoryRenderMode = renderMode;
            _maximumAlowedItemCount = maximumAlowedItemCount;
            _items = new List<IInventoryItem>(maximumAlowedItemCount);
        }

        public int InventoryItemCount => _items.Count;

        public InventoryRenderMode InventoryRenderMode { get; private set; }

        public bool IsInventoryFull
        {
            get
            {
                if (_maximumAlowedItemCount < 0)return false;
                return InventoryItemCount >= _maximumAlowedItemCount;
            }
        }

        public bool AddInventoryItem(IInventoryItem item)
        {
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