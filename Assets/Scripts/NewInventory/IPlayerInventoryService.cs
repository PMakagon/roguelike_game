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
        event Action onInventoryLoad;
        

        void InitializeInventory();

        IPlayerEquipment GetCurrentEquipment();

        EquipmentSlotProvider[] GetEquipmentSlots();

        CaseItemProvider GetCase();

        ContainerItemProvider GetContainer();

        FastSlotProvider GetFastSlots();

        BagItemProvider GetBag();
    }
}