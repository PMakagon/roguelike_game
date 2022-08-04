using System;
using UnityEngine;

namespace InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Battery", menuName = "Items/Battery")]
    public class BatteryItem : Item
    {
        [SerializeField] private float capacity;
        
        public float Capacity => capacity;

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