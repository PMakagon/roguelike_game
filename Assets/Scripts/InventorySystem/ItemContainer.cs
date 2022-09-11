using System;
using System.Collections.Generic;
using LiftGame.InventorySystem.Items;
using ModestTree;
using UnityEngine;

namespace LiftGame.InventorySystem
{
    [CreateAssetMenu(fileName = "ItemContainer", menuName = "Player/InventorySystem/ItemContainer")]
    public class ItemContainer : ScriptableObject,IItemContainer
    {
        [SerializeField] private string containerName;
        [SerializeField] private int capacity=10;
        private List<IItem> _items;
        // private IItem[] _items;

        private void Awake()
        {
            ClearContainer();
        }

        public int Capacity() => _items.Capacity;

        public bool HasNothing() => _items.IsEmpty();


        public bool IsFull() => _items.Count >= capacity;

        public void ClearContainer()
        {
            _items = new List<IItem>(capacity);
        }

        public bool AddItem(IItem item, int amount)
        {
            if (IsFull())
            {
                Debug.Log("inventory is full");
                return false;
            }

            if (_items.Contains(item))
            {
                if (item.ItemType != ItemType.Consumable)
                {
                    Debug.Log("Already Equipped " + item.Name);
                    return false;
                }
            }
            _items.Add(item);
            // onItemAdd?.Invoke();
            // onInventoryChange?.Invoke();
            return true;
        }
        
        public void RemoveItem(IItem item)
        {
            _items.Remove(item);
            // onInventoryChange?.Invoke();
        }

        public string ContainerName => containerName;

        public List<IItem> Items => _items;
    }
}