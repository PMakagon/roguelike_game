using UnityEngine;

namespace LiftGame.NewInventory.Items
{
    [CreateAssetMenu(fileName = "Key", menuName = "Player/InventorySystem/Items/Key")]
    public class Key : ItemDefinition
    {
        [SerializeField] private string keyCode;

        public string KeyCode
        {
            get => keyCode;
            set => keyCode = value;
        }

        public override ItemType ItemType => ItemType.Quest;

        private void Awake()
        {
            ItemType = ItemType.Key;
        }
    }
}