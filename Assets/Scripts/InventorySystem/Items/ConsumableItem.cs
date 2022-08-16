namespace InventorySystem.Items
{
    public class ConsumableItem : Item
    {
        public override ItemType ItemType
        {
            get => ItemType.Consumable;
        }

        private void Awake()
        {
            ItemType = ItemType.Consumable;
        }
    }
}