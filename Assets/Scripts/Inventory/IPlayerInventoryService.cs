using System;
using LiftGame.Inventory.Bag;
using LiftGame.Inventory.Case;
using LiftGame.Inventory.Container;
using LiftGame.Inventory.Equipment;
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
        EquipmentRepositoryManager GetEquipmentRepositoryManager(int index);
        EquipmentRepository[] GetEquipmentRepository();
        PowerCellSlotRepositoryManager GetPowerCellSlotRepositoryManager(int index);
        PowerCellSlotRepositoryManager[] GetAllPowerCellSlotRepositoryManagers();
        PowerCellSlotRepository[] GetPowerCellSlotRepository();
        PocketsItemRepository GetPocketsRepository();
        public InventoryData InventoryData { get; }
    }
}