using UnityEngine;

namespace LiftGame.InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Battery", menuName = "Items/Battery")]
    public class BatteryItem : ConsumableItem
    {
        [SerializeField] private float capacity;
        
        public float Capacity => capacity;

        public override void Use(InventoryData inventoryData)
        {
            inventoryData.PlayerPowerData.CurrentPower = capacity;
            base.Use(inventoryData);
        }
    }
}