using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "InventorySystem/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        [SerializeField] private int capacity = 3;
        [SerializeField]private List<Item> items;
    
        private bool _isNeedUpdate = false;
        public bool HasNothing() => items.Count == 0;
        public bool IsFull() => items.Count >= capacity;
        public void ClearInventory() => items.Clear();
    
    
        #region Methods
    
        private void Awake()
        {
            items = new List<Item>(capacity);
        }

        public bool AddItem(Item item)
        {
            if (IsFull())
            {
                Debug.Log("inventory is full");
                return false;
            }
            if (items.Contains(item))
            {
                Debug.Log("Already Equipped " + item.Name);
                return false;
            }
            items.Add(item);
            _isNeedUpdate = true;
            return true;
        }
    
        public void RemoveItem(Item item)
        {
            items.Remove(item);
            _isNeedUpdate = true;
        }
        #endregion
    
        #region Properties

        public bool IsNeedUpdate
        {
            get => _isNeedUpdate;
            set => _isNeedUpdate = value;
        }

        public List<Item> Items
        {
            get => items;
            set => items = value;
        }
    
        #endregion
    }
}