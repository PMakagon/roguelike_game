using UnityEngine;

namespace LiftGame.Inventory.Items
{
    [CreateAssetMenu(fileName = "PowerCell", menuName = "Player/InventorySystem/Items/PowerCell")]
    public class PowerCell : ItemDefinition
    {
        [SerializeField] private float maxCapacity;
        [SerializeField] private Sprite emptySprite;
        public bool IsEmpty() => CurrentPower <= 0;
        public float CurrentPower { get; set; }

        public float MaxCapacity => maxCapacity;
        public override ItemType ItemType => ItemType.PowerCell;

        public void BecomeEmpty()
        {
            sprite = emptySprite;
            name = "Empty Power Cell";
            Description = "Empty Power Cell.Could be useless";
            CurrentPower = 0;
        }
        private void Awake()
        {
            ItemType = ItemType.PowerCell;
            CurrentPower = maxCapacity;
        }
    }
}