using System;
using System.Collections.Generic;
using LiftGame.Inventory.Bag;
using LiftGame.Inventory.Case;
using LiftGame.Inventory.Container;
using LiftGame.Inventory.Core;
using LiftGame.Inventory.Equipment;
using LiftGame.Inventory.Items;
using LiftGame.Inventory.Pockets;
using LiftGame.Inventory.PowerCellSlots;
using LiftGame.PlayerEquipment;
using ModestTree;
using UnityEngine;

namespace LiftGame.Inventory
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Player/InventorySystem/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        //убрать в отдельный сервис
        [SerializeField] private EquipableContainerConfig bagSlotConfig;
        [SerializeField] private EquipableContainerConfig pocketsConfig;
        private EquipableContainerConfig _caseConfig;

        private ContainerItemRepository _currentContainer;
        private CaseItemRepository _caseRepository;
        private PocketsItemRepository _pockets;
        private EquipmentRepository[] _equipmentSlots;
        private PowerCellSlotRepository[] _powerCellSlots;
        private BagItemRepository _bagRepository;
        private PlayerEquipmentWorldView _currentEquipment;
        public event Action OnWorldItemAddedToBag;
        public event Action OnWorldItemAddedToCase;
        public event Action OnWorldItemAddedToEquipmentSlot;
        public event Action OnWorldItemAddedToPocket;

        public Action OnWorldContainerOpen; //убрать как нибудь
        public Action OnEquipmentAdd;
        public Action OnInventoryChange;

        private void Awake()
        {
            ResetData();
        }

        public void ResetData()
        {
            // _caseRepository = new CaseItemRepository(caseConfig.Widht, caseConfig.Height);
            _caseRepository = null;
            _pockets = new PocketsItemRepository();
            _equipmentSlots = new EquipmentRepository[2] { new(0), new(1) };
            _powerCellSlots = new PowerCellSlotRepository[3] { new(0), new(1), new(2) };
            _bagRepository = new BagItemRepository(bagSlotConfig.Widht, bagSlotConfig.Height);
            _currentContainer = null;
            _currentEquipment = null;
            Debug.Log("RESET INVENTORY DATA");
        }

        public bool TryToAddItem(IInventoryItem itemToAdd)
        {
            if (((ItemDefinition)itemToAdd).ItemType == ItemType.Equipment)
            {
                foreach (var slot in _equipmentSlots)
                {
                    if (!slot.IsEmpty) continue;
                    slot.AddEquipmentItem(itemToAdd);
                    OnWorldItemAddedToEquipmentSlot?.Invoke();
                    return true;
                }
            }

            if (_bagRepository != null)
            {
                if (_bagRepository.AddInventoryItem(itemToAdd))
                {
                    OnWorldItemAddedToBag?.Invoke();
                    return true;
                }
            }

            if (_caseRepository != null && _caseRepository.IsInRange)
            {
                if (_caseRepository.AddInventoryItem(itemToAdd))
                {
                    OnWorldItemAddedToCase?.Invoke();
                    return true;
                }
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
                if (((ItemDefinition)item).Name == itemName)
                {
                    return item;
                }
            }

            return null;
        }

        public List<IInventoryItem> GetAllItems()
        {
            List<IInventoryItem> allItems = new List<IInventoryItem>();
            if (_caseRepository != null) allItems.AddRange(_caseRepository.GetAllItems());
            allItems.AddRange(_bagRepository.GetAllItems());
            allItems.AddRange(_pockets.GetAllItems());
            if (allItems.IsEmpty()) Debug.Log("INVENTORY IS EMPTY");
            return allItems;
        }

        public BagItemRepository BagRepository
        {
            get => _bagRepository;
            set => _bagRepository = value;
        }

        public CaseItemRepository CaseRepository
        {
            get => _caseRepository;
            set => _caseRepository = value;
        }

        public ContainerItemRepository CurrentContainer
        {
            get => _currentContainer;
            set => _currentContainer = value;
        }

        public PocketsItemRepository PocketsRepository
        {
            get => _pockets;
            set => _pockets = value;
        }

        public EquipmentRepository[] EquipmentSlots
        {
            get => _equipmentSlots;
            set => _equipmentSlots = value;
        }

        public PowerCellSlotRepository[] PowerCellSlots
        {
            get => _powerCellSlots;
            set => _powerCellSlots = value;
        }

        public PlayerEquipmentWorldView CurrentEquipment
        {
            get => _currentEquipment;
            set => _currentEquipment = value;
        }

        public EquipableContainerConfig BagSlotConfig
        {
            get => bagSlotConfig;
            set => bagSlotConfig = value;
        }

        public EquipableContainerConfig PocketsConfig
        {
            get => pocketsConfig;
            set => pocketsConfig = value;
        }

        public EquipableContainerConfig CaseConfig
        {
            get => _caseConfig;
            set => _caseConfig = value;
        }
    }
}