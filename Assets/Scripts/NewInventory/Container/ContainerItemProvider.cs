using System.Collections.Generic;
using FarrokhGames.Inventory;
using LiftGame.NewInventory.Items;

namespace LiftGame.NewInventory.Container
{
    public class ContainerItemProvider : IInventoryProvider
    {
        private ContainerConfig _config;
        private List<IInventoryItem> _items;
        private readonly int _maximumItemCount;
        public bool isLootSpawned = false;

        public ContainerItemProvider(ContainerConfig config)
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
            foreach (var itemDefinition in _config.AllowedItems)
            {
                if (itemDefinition.ItemType == ((ItemDefinition)item).ItemType)
                {
                    return true;
                }
            }
            return false;
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