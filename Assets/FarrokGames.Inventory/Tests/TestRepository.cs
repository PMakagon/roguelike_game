using System.Collections.Generic;
using FarrokGames.Inventory.Runtime;

namespace FarrokhGames.Inventory
{
    public class TestRepository : IInventoryRepository
    {
        private readonly List<IInventoryItem> _items = new List<IInventoryItem>();
        private readonly int _maximumAlowedItemCount;

        /// <summary>
        /// CTOR
        /// </summary>
        public TestRepository(InventoryRenderMode renderMode = InventoryRenderMode.Grid, int maximumAlowedItemCount = -1)
        {
            InventoryRenderMode = renderMode;
            _maximumAlowedItemCount = maximumAlowedItemCount;
        }

        public int InventoryItemCount => _items.Count;

        public InventoryRenderMode InventoryRenderMode { get; }

        public bool IsInventoryFull
        {
            get
            {
                if (_maximumAlowedItemCount < 0) return false;
                return InventoryItemCount < _maximumAlowedItemCount;
            }
        }

        public bool AddInventoryItem(IInventoryItem item)
        {
            if (_items.Contains(item)) return false;
            _items.Add(item);
            return true;
        }

        public bool DropInventoryItem(IInventoryItem item) => RemoveInventoryItem(item);
        public IInventoryItem GetInventoryItem(int index) => _items[index];
        
        public List<IInventoryItem> GetAllItems()
        {
            return _items;
        }

        public bool CanAddInventoryItem(IInventoryItem item) => true;
        public bool CanRemoveInventoryItem(IInventoryItem item) => true;
        public bool CanDropInventoryItem(IInventoryItem item) => true;
        public bool RemoveInventoryItem(IInventoryItem item) => _items.Remove(item);
    }
}