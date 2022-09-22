using System;
using LiftGame.NewInventory.Bag;
using LiftGame.NewInventory.Case;
using LiftGame.NewInventory.Container;
using LiftGame.NewInventory.Equipment;
using LiftGame.NewInventory.FastSlots;
using LiftGame.PlayerCore;
using LiftGame.PlayerEquipment;
using Zenject;

namespace LiftGame.NewInventory
{
    public class PlayerInventoryService : IPlayerInventoryService
    {
        private InventoryData _inventoryData;
        
        public event Action onInventoryLoad;

        [Inject]
        public PlayerInventoryService(IPlayerData playerData)
        {
            _inventoryData = playerData.GetInventoryData();
        }
        
        public void InitializeInventory()
        {
            _inventoryData.ResetData();
            onInventoryLoad?.Invoke();
        }

        public IPlayerEquipment GetCurrentEquipment()
        {
            return _inventoryData.CurrentEquipment;
        }

        public EquipmentSlotProvider[] GetEquipmentSlots()
        {
            return _inventoryData.EquipmentSlots;
        }

        public CaseItemProvider GetCase()
        {
            return _inventoryData.CaseInventory;
        }

        public ContainerItemProvider GetContainer()
        {
            return _inventoryData.CurrentContainer;
        }

        public FastSlotProvider GetFastSlots()
        {
            return _inventoryData.FastSlots;
        }

        public BagItemProvider GetBag()
        {
            return _inventoryData.BagProvider;
        }
    }
}