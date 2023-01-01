using System.Collections.Generic;
using FarrokGames.Inventory.Runtime;
using FarrokhGames.Inventory;
using LiftGame.NewInventory.Items;
using UnityEngine;

namespace LiftGame.NewInventory.FastSlots
{
    public class PocketsItemRepository : IInventoryRepository
    {
        private List<IInventoryItem> _items = new List<IInventoryItem>(5);
        
        public int InventoryItemCount => _items.Count;

        public InventoryRenderMode InventoryRenderMode => InventoryRenderMode.Grid;

        public bool IsInventoryFull
        {
            get
            {
                for (int i = 0; i < _items.Count; i++)
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
            if (IsInventoryFull) return false;
            if (!_items.Contains(item))
            {
                _items.Add(item);
                Debug.Log("ITEM ADDED" + item.position);
                return true;
            }
            // for (int i = 0; i < _items.Count; i++)
            // {
            //     if (item == _items[i])
            //     {
            //         return false;
            //     }
            //
            //     if (_items[i]==null)
            //     {
            //         _items[i] = item;
            //         return true;
            //     }
            // }
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
            for (int i = 0; i < _items.Count; i++)
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