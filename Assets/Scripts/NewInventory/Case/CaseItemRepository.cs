using System.Collections.Generic;
using FarrokGames.Inventory.Runtime;
using FarrokhGames.Inventory;
using UnityEngine;

namespace LiftGame.NewInventory.Case
{
    public class CaseItemRepository : IInventoryRepository
    {
        private readonly List<IInventoryItem> _items;
        private readonly int _maximumItemCount;
        private bool _isInRange;
        private bool _isBinded;

        public CaseItemRepository(int widht, int height)
        {
            _maximumItemCount = height*widht;
            _items = new List<IInventoryItem>(_maximumItemCount);
        }

        public bool IsBinded
        {
            get => _isBinded;
            set => _isBinded = value;
        }

        public bool IsInRange
        {
            get => _isInRange;
            set => _isInRange = value;
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
                Debug.Log("Item ADDED to BAG");
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