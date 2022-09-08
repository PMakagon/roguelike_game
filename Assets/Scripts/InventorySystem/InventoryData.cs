using System;
using System.Collections.Generic;
using LiftGame.InventorySystem.Items;
using LiftGame.PlayerCore.PlayerPowerSystem;
using LiftGame.PlayerEquipment;
using UnityEngine;

namespace LiftGame.InventorySystem
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Player/InventorySystem/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        // [SerializeField] private int capacity=10;
        // private List<IItem> _items;
        // private string _containerName;
        // private List<IItem> _containerItems;
        [SerializeField] private ItemContainer testPlayerInventoryToUse;
        private IItemContainer _currentContainer;
        private IItemContainer _inventoryContainer;
        private IItem[] _fastSlots;
        private EquipmentItem[] _equipmentSlots;
        private IPlayerEquipment _currentEquipment;

        public Action onItemAdd;
        public Action onContainerOpen;
        public Action onEquipmentAdd;
        public Action onInventoryChange;

        [ContextMenu("SwitchToTestInventory")]
        public void SwitchToTestInventory()
        {
            _inventoryContainer = testPlayerInventoryToUse;
        }
        
        public IItemContainer CurrentContainer
        {
            get => _currentContainer;
            set => _currentContainer = value;
        }

        public IItemContainer InventoryContainer
        {
            get => _inventoryContainer ??= CreateInstance<ItemContainer>();
            set => _inventoryContainer = value;
        }

        public IItem[] FastSlots
        {
            get => _fastSlots;
            set => _fastSlots = value;
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