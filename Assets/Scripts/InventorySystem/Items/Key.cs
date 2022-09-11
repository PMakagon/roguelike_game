using UnityEngine;

namespace LiftGame.InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Key", menuName = "Player/InventorySystem/Items/Key")]
    public class Key : Item
    {
        [SerializeField] private string keyCode;

        public string KeyCode
        {
            get => keyCode;
            set => keyCode = value;
        }

        public override ItemType ItemType
        {
            get => ItemType.Key;
        }
        
        private void Awake()
        {
            ItemType = ItemType.Key;
        }
    }
}