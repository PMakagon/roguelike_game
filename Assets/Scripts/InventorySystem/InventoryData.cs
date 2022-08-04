using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using InventorySystem.Items;
using PlayerEquipment;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "InventorySystem/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        public string savePath;
        [SerializeField] private int capacity = 10;
        private List<IItem> _items;
        private string _containerName;
        private List<IItem> _containerItems;
        private EquipmentItem[] _equipmentSlots;
        private IPlayerEquipment _currentEquipment;

        public Action onItemAdd;
        public Action onContainerOpen;
        public Action onEquipmentAdd;
        public Action onInventoryChange;


        #region Methods

        public int GetCapacity() => capacity;
        public bool HasNothing() => _items.Count == 0;

        public bool IsFull() => _items.Count >= capacity;

        public void ClearContainer()
        {
            _containerItems = null;
            _containerName = null;
        }

        private void Awake()
        {
            _items = new List<IItem>(capacity);
        }

        public void ResetData()
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
            onItemAdd?.Invoke();
            onInventoryChange?.Invoke();
            return true;
        }
        

        public void RemoveItem(IItem item)
        {
            _items.Remove(item);
            onInventoryChange?.Invoke();
        }

        #endregion
        
        
        #region Properties

        public List<IItem> Items
        {
            get => _items;
            set => _items = value;
        }

        public string ContainerName
        {
            get => _containerName;
            set => _containerName = value;
        }

        public List<IItem> ContainerItems
        {
            get => _containerItems;
            set => _containerItems = value;
        }

        public EquipmentItem[] EquipmentSlots
        {
            get => _equipmentSlots;
            set => _equipmentSlots = value;
        }
        
        public IPlayerEquipment CurrentEquipment
        {
            get => _currentEquipment;
            set => _currentEquipment = value;
        }

        #endregion
    }
}