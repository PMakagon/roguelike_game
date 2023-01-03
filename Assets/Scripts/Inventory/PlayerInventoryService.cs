using System;
using LiftGame.Inventory.Bag;
using LiftGame.Inventory.Case;
using LiftGame.Inventory.Container;
using LiftGame.Inventory.Equipment;
using LiftGame.Inventory.Pockets;
using LiftGame.PlayerCore;
using LiftGame.PlayerEquipment;
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
        public event Action OnInventoryLoad; 
        public event Action OnInventoryOpen;
        public event Action OnInventoryClose;

        [Inject]
        public PlayerInventoryService(IPlayerData playerData)
        {
            _inventoryData = playerData.GetInventoryData();
        }

        public void InitializeInventory()
        {
            _inventoryData.ResetData();
            OnInventoryLoad?.Invoke();
        }

        public void SetInventoryOpen(bool state)
        {
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
            if (_bagRepositoryManager==null)
            {
                _bagRepositoryManager =  new BagRepositoryManager(GetBagRepository(), _inventoryData.BagSlotConfig.Widht,
                    _inventoryData.BagSlotConfig.Height);
                _inventoryData.OnWorldItemAddedToBag += _bagRepositoryManager.Rebuild;
            }
            return _bagRepositoryManager;
        }

        public BagItemRepository GetBagRepository()
        {
            return _inventoryData.BagRepository;
        }

        public CaseRepositoryManager GetCaseRepositoryManager()
        {
            if (_caseRepositoryManager==null)
            {
                _caseRepositoryManager = new CaseRepositoryManager(GetCaseRepository(), _inventoryData.CaseConfig.Widht,
                    _inventoryData.CaseConfig.Height);
                _inventoryData.OnWorldItemAddedToCase += _caseRepositoryManager.Rebuild;
            }
            return _caseRepositoryManager;
        }

        public CaseItemRepository GetCaseRepository()
        {
            return _inventoryData.CaseRepository;
        }

        public ContainerRepositoryManager GetContainerRepositoryManager()
        {
            if (_containerRepositoryManager==null)
            {
               _containerRepositoryManager =  new ContainerRepositoryManager();
            }
            return _containerRepositoryManager;
        }

        public ContainerItemRepository GetContainerRepository()
        {
            return _inventoryData.CurrentContainer;
        }

        public EquipmentRepositoryManager GetEquipmentRepositoryManager(int index)
        {
            if (_equipmentRepositoryManagers[index]==null)
            {
                _equipmentRepositoryManagers[index] =  new EquipmentRepositoryManager(GetEquipmentRepository()[index]);
                _inventoryData.OnWorldItemAddedToEquipmentSlot += _equipmentRepositoryManagers[index].InvokeOnWorldItemAdded;
            }
            return _equipmentRepositoryManagers[index];
        }

        public EquipmentRepository[] GetEquipmentRepository()
        {
            return _inventoryData.EquipmentSlots;
        }

        public PocketsItemRepository GetPocketsRepository()
        {
            return _inventoryData.PocketsRepository;
        }

        public InventoryData InventoryData => _inventoryData;
    }
}