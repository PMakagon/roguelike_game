using System;
using System.Collections.Generic;
using System.Linq;
using LiftGame.Inventory.Bag;
using LiftGame.Inventory.Case;
using LiftGame.Inventory.Container;
using LiftGame.Inventory.Core;
using LiftGame.Inventory.Equipment;
using LiftGame.Inventory.Items;
using LiftGame.Inventory.Pockets;
using LiftGame.Inventory.PowerCellSlots;
using LiftGame.PlayerCore;
using LiftGame.PlayerEquipment;
using LiftGame.ProxyEventHolders.Player;
using ModestTree;
using UnityEngine;
using Zenject;

namespace LiftGame.Inventory
{
    public class PlayerInventoryService : IPlayerInventoryService
    {
        private readonly InventoryData _inventoryData;
        private BagRepositoryManager _bagRepositoryManager;
        private CaseRepositoryManager _caseRepositoryManager;
        private ContainerRepositoryManager _containerRepositoryManager;
        private EquipmentRepositoryManager[] _equipmentRepositoryManagers = new EquipmentRepositoryManager[2];
        private PowerCellSlotRepositoryManager[] _powerCellRepositoryManagers = new PowerCellSlotRepositoryManager[3];
        private List<IInventoryItem> _cachedItems = new List<IInventoryItem>();
        private bool _isChanged;
        public event Action OnInventoryLoad;
        public event Action OnInventoryOpen;
        public event Action OnInventoryClose;

        [Inject]
        public PlayerInventoryService(IPlayerData playerData)
        {
            _inventoryData = playerData.GetInventoryData();
        }

        public bool IsInventoryOpen { get; private set; }

        public void InitializeInventory()
        {
            _inventoryData.ResetData();
            OnInventoryLoad?.Invoke();
            PlayerInventoryEventHolder.OnInventoryChange += () => _isChanged = true;
            _cachedItems = new List<IInventoryItem>();
        }

        public void SetInventoryOpen(bool state)
        {
            IsInventoryOpen = state;
            if (state)
            {
                OnInventoryOpen?.Invoke();
            }
            else
            {
                OnInventoryClose?.Invoke();
            }
        }

        public PlayerEquipmentWorldView GetCurrentEquipment()
        {
            return _inventoryData.CurrentEquipment;
        }

        public void SetCurrentEquipment(PlayerEquipmentWorldView equipment)
        {
            _inventoryData.CurrentEquipment = equipment;
        }

        public BagRepositoryManager GetBagRepositoryManager()
        {
            if (_bagRepositoryManager == null)
            {
                _bagRepositoryManager = new BagRepositoryManager(GetBagRepository(), _inventoryData.BagSlotConfig.Widht,
                    _inventoryData.BagSlotConfig.Height);
            }

            return _bagRepositoryManager;
        }

        public BagItemRepository GetBagRepository()
        {
            return _inventoryData.BagRepository;
        }

        public CaseRepositoryManager GetCaseRepositoryManager()
        {
            if (_caseRepositoryManager == null)
            {
                _caseRepositoryManager = new CaseRepositoryManager(GetCaseRepository(), _inventoryData.CaseConfig.Widht,
                    _inventoryData.CaseConfig.Height);
            }
            return _caseRepositoryManager;
        }

        public CaseItemRepository GetCaseRepository()
        {
            return _inventoryData.CaseRepository;
        }

        public ContainerRepositoryManager GetContainerRepositoryManager()
        {
            if (_containerRepositoryManager == null)
            {
                _containerRepositoryManager = new ContainerRepositoryManager();
            }

            return _containerRepositoryManager;
        }

        public ContainerItemRepository GetContainerRepository()
        {
            return _inventoryData.CurrentContainer;
        }

        public void SetCurrentContainerRepository(ContainerItemRepository containerRepository)
        {
            _inventoryData.CurrentContainer = containerRepository;
        }

        public EquipmentRepositoryManager GetEquipmentRepositoryManager(int index)
        {
            if (_equipmentRepositoryManagers[index] == null)
            {
                _equipmentRepositoryManagers[index] = new EquipmentRepositoryManager(GetEquipmentRepository()[index]);
            }
            return _equipmentRepositoryManagers[index];
        }

        public EquipmentRepository[] GetEquipmentRepository()
        {
            return _inventoryData.EquipmentSlots;
        }

        public PowerCellSlotRepositoryManager GetPowerCellSlotRepositoryManager(int index)
        {
            if (_powerCellRepositoryManagers[index] == null)
            {
                _powerCellRepositoryManagers[index] =
                    new PowerCellSlotRepositoryManager(GetPowerCellSlotRepository()[index]);
            }

            return _powerCellRepositoryManagers[index];
        }

        public PowerCellSlotRepositoryManager[] GetAllPowerCellSlotRepositoryManagers()
        {
            return _powerCellRepositoryManagers;
        }

        public PowerCellSlotRepository[] GetPowerCellSlotRepository()
        {
            return _inventoryData.PowerCellSlots;
        }

        public PocketsItemRepository GetPocketsRepository()
        {
            return _inventoryData.PocketsRepository;
        }

        public List<IInventoryItem> GetAllItems()
        {
            if (!_isChanged) return _cachedItems;
            _cachedItems = new List<IInventoryItem>();
            if (GetCaseRepository() != null) _cachedItems.AddRange(GetCaseRepository().GetAllItems());
            _cachedItems.AddRange(GetBagRepository().GetAllItems());
            foreach (var equipmentRepository in GetEquipmentRepository())
            {
                _cachedItems.Add(equipmentRepository.GetInventoryItem());
            }
            _cachedItems.AddRange(GetBagRepository().GetAllItems());
            _cachedItems.AddRange(GetPocketsRepository().GetAllItems());
            if (_cachedItems.IsEmpty()) Debug.Log("INVENTORY IS EMPTY");
            _isChanged = false;
            return _cachedItems;
        }

        public bool TryToAddItem(IInventoryItem itemToAdd)
        {
            if (((ItemDefinition)itemToAdd).ItemType == ItemType.Equipment)
            {
                foreach (var slot in GetEquipmentRepository())
                {
                    if (!slot.IsEmpty) continue;
                    slot.AddEquipmentItem(itemToAdd);
                    PlayerInventoryEventHolder.BroadcastOnItemAddedToEquipmentSlot();
                    return true;
                }
            }

            if (GetBagRepository() != null)
            {
                if (GetBagRepository().AddInventoryItem(itemToAdd))
                {
                    PlayerInventoryEventHolder.BroadcastOnItemAddedToBag();
                    return true;
                }
            }

            if (GetCaseRepository() != null && GetCaseRepository().IsInRange)
            {
                if (GetCaseRepository().AddInventoryItem(itemToAdd))
                {
                    PlayerInventoryEventHolder.BroadcastOnItemAddedToCase();
                    return true;
                }
            }

            return false;
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

        public int CountItemByDefinition(ItemDefinition itemToCount)
        {
            if (GetAllItems().IsEmpty())
            {
                Debug.Log("SOSOSOSOSOS");
            }
            return GetAllItems().Count(item => ((ItemDefinition)item).Name.Equals(itemToCount.Name));
        }

        public bool TryToRemoveItem(IInventoryItem itemToRemove) //REWORK
        {
            if (((ItemDefinition)itemToRemove).ItemType == ItemType.Equipment)
            {
                if (_equipmentRepositoryManagers.Any(equipmentRepository => equipmentRepository.TryRemove(itemToRemove)))
                {
                    PlayerInventoryEventHolder.BroadcastOnInventoryChange();
                    return true;
                }
            }
            if (_bagRepositoryManager.TryRemove(itemToRemove) || _caseRepositoryManager.TryRemove(itemToRemove))
            {
                PlayerInventoryEventHolder.BroadcastOnInventoryChange();
                return true;
            }
            return false;
        }

        public int RemoveAllByDefinition(ItemDefinition itemToRemove)
        {
            var removedAmount = 0;
            foreach (var item in GetAllItems())
            {
                if ( TryToRemoveItem(item)) removedAmount++;
            }
            if (removedAmount > 0) PlayerInventoryEventHolder.BroadcastOnInventoryChange();
            return removedAmount;
        }

        public int RemoveAllByDefinition(ItemDefinition itemToRemove, int limit)
        {
            var removedAmount = 0;
            foreach (var item in GetAllItems())
            {
                if ( TryToRemoveItem(item)) removedAmount++;
                if (removedAmount == limit) break;
            }
            if (removedAmount > 0) PlayerInventoryEventHolder.BroadcastOnInventoryChange();
            return removedAmount;
        }

        public InventoryData InventoryData => _inventoryData;
    }
}