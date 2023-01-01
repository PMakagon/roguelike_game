using UnityEngine;

namespace LiftGame.NewInventory.Items
{
    [CreateAssetMenu(fileName = "BetaBlocker", menuName = "Player/InventorySystem/Items/BetaBlocker")]
    public class BetaBlocker : ItemDefinition //useable item
    {
        public override ItemType ItemType => ItemType.Consumable;

        private void Awake()
        {
            ItemType = ItemType.Consumable;
        }
    }
}