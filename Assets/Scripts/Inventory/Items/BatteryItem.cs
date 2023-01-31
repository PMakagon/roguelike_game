using LiftGame.Ui;
using UnityEngine;

namespace LiftGame.Inventory.Items
{
    [CreateAssetMenu(fileName = "Battery", menuName = "Player/InventorySystem/Items/Battery")]
    public class BatteryItem : ConsumableItem
    {
        [SerializeField] private float capacity;
        
        public float Capacity
        {
            get => capacity;
            set => capacity = value;
        }

        public override void Use(InventoryItemInteractor interactor)
        {
           interactor.PowerService.AddPower((int)capacity);
        }
    }
}