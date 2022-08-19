using LiftGame.PlayerEquipment;

namespace LiftGame.InventorySystem.Items
{
    public class EquipmentItem : Item
    { 
        public IPlayerEquipment PlayerEquipment { get; set; }

        public override ItemType ItemType
        {
            get => ItemType.Equipment;
        }
        
        private void Awake()
        {
            ItemType = ItemType.Equipment;
        }
    }
}