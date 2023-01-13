using LiftGame.FPSController.InteractionSystem;
using LiftGame.PlayerEquipment;
using UnityEngine;

namespace LiftGame.Inventory.Items
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "Player/InventorySystem/Items/Equipment")]
    public class EquipmentItem : ItemDefinition
    {
        [SerializeField] private PlayerEquipmentWorldView equipmentPrefab;
        private EquipmentData _equipmentData;

        public EquipmentData EquipmentData
        {
            get  => _equipmentData ?? new EquipmentData();
            set => _equipmentData = value;
        }

        public override ItemType ItemType => ItemType.Equipment;

        public PlayerEquipmentWorldView EquipmentPrefab => equipmentPrefab;
    }
}