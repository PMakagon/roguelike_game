using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Battery", menuName = "Items/Battery")]
    public class BatteryItem : ConsumableItem
    {
        [SerializeField] private float capacity;
        
        public float Capacity => capacity;

        public override void Use(IPlayerData playerData)
        {
            var powerData = playerData.GetPowerData();
            powerData.CurrentPower = capacity;
            base.Use(playerData);
        }
    }
}