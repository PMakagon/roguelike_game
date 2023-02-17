using LiftGame.Ui;

namespace LiftGame.Inventory.Items
{
    public interface IUseableItem
    {
        void Use(InventoryItemInteractor interactor);
    }
}