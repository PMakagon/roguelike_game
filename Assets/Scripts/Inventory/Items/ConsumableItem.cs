using LiftGame.Ui;
using UnityEngine;

namespace LiftGame.Inventory.Items
{
    public abstract class ConsumableItem : ItemDefinition, IUseableItem
    {
        [SerializeField] private bool destroyOnUse;

        public bool DestroyOnUse => destroyOnUse;
        public override ItemType ItemType => ItemType.Consumable;
        private void Awake()
        {
            ItemType = ItemType.Consumable;
        }
        
        public virtual void Use(InventoryItemInteractor interactor)
        {
            Debug.Log("CONSUMABLE ITEM USED ");
        }
    }
}