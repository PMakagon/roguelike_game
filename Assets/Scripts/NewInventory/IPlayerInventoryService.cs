using System;
using LiftGame.NewInventory.Bag;
using LiftGame.NewInventory.Case;
using LiftGame.NewInventory.Container;
using LiftGame.NewInventory.Equipment;
using LiftGame.NewInventory.FastSlots;
using LiftGame.PlayerEquipment;

namespace LiftGame.NewInventory
{
    public interface IPlayerInventoryService
    {
        event Action OnInventoryLoad;
        public event Action OnInventoryOpen;
        public event Action OnInventoryClose;

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
        PocketsItemRepository GetPocketsRepository();
        public InventoryData InventoryData { get; }
    }
}