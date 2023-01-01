using UnityEngine;

namespace LiftGame.NewInventory.Items
{
    [CreateAssetMenu(fileName = "Battery", menuName = "Player/InventorySystem/Items/Battery")]
    public class BatteryItem : EquipmentItem
    {
        [SerializeField] private float capacity;
        public override ItemType ItemType => ItemType.Consumable;
        public float Capacity
        {
            get => capacity;
            set => capacity = value;
        }

        // public override void Use(IPlayerData playerData)
        // {
        //     var powerData = playerData.GetPowerData();
        //     powerData.CurrentPower = capacity;
        //     base.Use(playerData);
        // }
    }
}