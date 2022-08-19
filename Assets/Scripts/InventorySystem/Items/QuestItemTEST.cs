using UnityEngine;

namespace LiftGame.InventorySystem.Items
{
    [CreateAssetMenu(fileName = "QuestItemTest", menuName = "Quest/Test")]
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