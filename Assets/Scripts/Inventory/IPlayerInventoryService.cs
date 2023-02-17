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

namespace LiftGame.Inventory
{
    public interface IPlayerInventoryService
    {
        event Action OnInventoryLoad;
        public event Action OnInventoryOpen;
        public event Action OnInventoryClose;

        bool IsInventoryOpen { get; }
        void InitializeInventory();
        void SetInventoryOpen(bool state);
        void SetCurrentEquipment(PlayerEquipmentWorldView equipment);
        PlayerEquipmentWorldView GetCurrentEquipment();
        public BagRepositoryManager GetBagRepositoryManager();
        BagItemRepository GetBagRepository();
        public CaseRepositoryManager GetCaseRepositoryManager();
        CaseItemRepository GetCaseRepository();
        public ContainerRepositoryManager GetContainerRepositoryManager();
        ContainerItemRepository GetContainerRepository();
        void SetCurrentContainerRepository(ContainerItemRepository containerRepository);
        EquipmentRepositoryManager GetEquipmentRepositoryManager(int index);
        EquipmentRepository[] GetEquipmentRepository();
        PowerCellSlotRepositoryManager GetPowerCellSlotRepositoryManager(int index);
        PowerCellSlotRepositoryManager[] GetAllPowerCellSlotRepositoryManagers();
        PowerCellSlotRepository[] GetPowerCellSlotRepository();
        PocketsItemRepository GetPocketsRepository();
        List<IInventoryItem> GetAllItems();
        bool TryToAddItem(IInventoryItem itemToAdd);
        IInventoryItem GetItemByName(string itemName);
        int CountItemByDefinition(ItemDefinition itemToCount);
        bool TryToRemoveItem(IInventoryItem itemToRemove);
        int RemoveAllByDefinition(ItemDefinition itemToRemove);
        int RemoveAllByDefinition(ItemDefinition itemToRemove,int limit);
        InventoryData InventoryData { get; }
    }
}