using System;
using System.Collections.Generic;
using FarrokhGames.Inventory;
using LiftGame.NewInventory.Bag;
using LiftGame.NewInventory.Case;
using LiftGame.NewInventory.Container;
using LiftGame.NewInventory.Equipment;
using LiftGame.NewInventory.FastSlots;
using LiftGame.NewInventory.Items;
using LiftGame.PlayerEquipment;
using UnityEngine;

namespace LiftGame.NewInventory
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Player/InventorySystem/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        private ContainerItemProvider _currentContainer;
        private CaseItemProvider _caseInventory;
        private FastSlotProvider _fastSlots;
        private EquipmentSlotProvider[] _equipmentSlots;
        private BagItemProvider _bagProvider;
        private IPlayerEquipment _currentEquipment;

        public Action onItemAdd;

        public Action onContainerOpen;

        public Action onEquipmentAdd;

        public Action onInventoryChange;

        private void Awake()
        {
            ResetData();
        }

        public void ResetData()
        {
            _caseInventory = new CaseItemProvider(6, 5);
            _fastSlots = new FastSlotProvider();
            _equipmentSlots = new EquipmentSlotProvider[2];
            _bagProvider = new BagItemProvider(3, 3);
            _currentContainer = null;
            _currentEquipment = null;
            Debug.Log("RESET DATA");
        }

        public bool TryToAddItem(IInventoryItem itemToAdd)
        {
            if (_bagProvider == null && !_caseInventory.IsInRange) return false;
            if (_bagProvider!=null)
            {
                if (_bagProvider.AddInventoryItem(itemToAdd)) return true;
            }

            if (_caseInventory.IsInRange)
            {
                if (_caseInventory.AddInventoryItem(itemToAdd)) return true;
            }
            return false;
        }

        public bool TryToRemoveItem(IInventoryItem itemToAdd)
        {
            var items = GetAllItems();
            return items.Remove(itemToAdd);
        }

        public IInventoryItem GetItemByName(string itemName)
        {
            var items = GetAllItems();
            foreach (var item in items)
            {
                if ( ((ItemDefinition)item).Name == itemName)
                {
                    return item;
                }
            }
            return null;
        }

        public List<IInventoryItem> GetAllItems()
        {
            List<IInventoryItem> allItems = new List<IInventoryItem>();
            if (_caseInventory.IsInRange) allItems.AddRange(_caseInventory.GetAllItems());
            allItems.AddRange(_bagProvider.GetAllItems());
            allItems.AddRange(_fastSlots.GetAllItems());
            return allItems;
        }

        public BagItemProvider BagProvider
        {
            get => _bagProvider;
            set => _bagProvider = value;
        }

        public CaseItemProvider CaseInventory
        {
            get => _caseInventory;
            set => _caseInventory = value;
        }

        public ContainerItemProvider CurrentContainer
        {
            get => _currentContainer;
            set => _currentContainer = value;
        }

        public FastSlotProvider FastSlots
        {
            get => _fastSlots;
            set => _fastSlots = value;
        }

        public EquipmentSlotProvider[] EquipmentSlots
        {
            get => _equipmentSlots;
            set => _equipmentSlots = value;
        }

        public IPlayerEquipment CurrentEquipment
        {
            get => _currentEquipment;
            set => _currentEquipment = value;
        }

        public Action OnItemAdd
        {
            get => onItemAdd;
            set => onItemAdd = value;
        }

        public Action OnContainerOpen
        {
            get => onContainerOpen;
            set => onContainerOpen = value;
        }

        public Action OnEquipmentAdd
        {
            get => onEquipmentAdd;
            set => onEquipmentAdd = value;
        }

        public Action OnInventoryChange
        {
            get => onInventoryChange;
            set => onInventoryChange = value;
        }
    }
}