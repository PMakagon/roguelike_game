using UnityEngine;

namespace LiftGame.Inventory.Items
{
    public abstract class ConsumableItem : ItemDefinition, IUseableItem
    {
        public override ItemType ItemType => ItemType.Consumable;
        private void Awake()
        {
            ItemType = ItemType.Consumable;
        }
        
        public virtual void Use()
        {
            Debug.Log("CONSUMABLE ITEM USED ");
        }
    }
}