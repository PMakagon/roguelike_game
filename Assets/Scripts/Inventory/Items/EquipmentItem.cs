using LiftGame.PlayerEquipment;
using UnityEngine;

namespace LiftGame.Inventory.Items
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "Player/InventorySystem/Items/Equipment")]
    public class EquipmentItem : ItemDefinition
    {
        [SerializeField] private PlayerEquipmentWorldView equipmentPrefab;
        
        public override ItemType ItemType => ItemType.Equipment;

        public PlayerEquipmentWorldView EquipmentPrefab => equipmentPrefab;
    }
}