using UnityEngine;

namespace LiftGame.InventorySystem.Items
{
    [CreateAssetMenu(fileName = "QuestItemTest", menuName = "Player/InventorySystem/Items/QuestTest")]
    public class QuestItemTEST : Item
    {
        public override ItemType ItemType
        {
            get => ItemType.Quest;
        }
        
        private void Awake()
        {
            ItemType = ItemType.Quest;
        }
    }
}