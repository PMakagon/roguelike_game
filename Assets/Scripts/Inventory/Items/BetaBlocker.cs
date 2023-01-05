using UnityEngine;

namespace LiftGame.Inventory.Items
{
    [CreateAssetMenu(fileName = "BetaBlocker", menuName = "Player/InventorySystem/Items/BetaBlocker")]
    public class BetaBlocker : ConsumableItem 
    {
        public override void Use()
        {
            base.Use();
        }
    }
}