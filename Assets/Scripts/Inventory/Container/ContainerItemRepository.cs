using System.Collections.Generic;
using LiftGame.Inventory.Core;

namespace LiftGame.Inventory.Container
{
    public class ContainerItemRepository : IInventoryRepository
    {
        private ContainerConfig _config;
        private List<IInventoryItem> _items;
        private readonly int _maximumItemCount;
        public bool isLootSpawned = false;

        public ContainerItemRepository(ContainerConfig config)
        {
            _config = config;
            InventoryRenderMode =config.RenderMode;
            _maximumItemCount = config.Height*config.Widht;
            _items = new List<IInventoryItem>(_maximumItemCount);
        }

        public ContainerConfig Config => _config;

        public int InventoryItemCount => _items.Count;

        public InventoryRenderMode InventoryRenderMode { get; private set; }

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