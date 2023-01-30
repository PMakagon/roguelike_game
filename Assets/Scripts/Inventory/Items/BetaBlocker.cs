using LiftGame.Ui;
using UnityEngine;

namespace LiftGame.Inventory.Items
{
    [CreateAssetMenu(fileName = "BetaBlocker", menuName = "Player/InventorySystem/Items/BetaBlocker")]
    public class BetaBlocker : ConsumableItem 
    {
        public override void Use(InventoryItemInteractor interactor)
        {
            if (interactor.MentalService.IsStressChangeEnabled)
            {
                interactor.MentalService.ReduceStress(70);
            }
           
        }
    }
}