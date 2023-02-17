using UnityEngine;

namespace LiftGame.Inventory
{
    [CreateAssetMenu(fileName = "EquipableContainerConfig", menuName = "Player/InventorySystem/EquipableContainerConfig")]
    public class EquipableContainerConfig : ScriptableObject
    {
        [SerializeField] private EquipableContainerType containerType;
        [SerializeField] private string containerName;
        [SerializeField] private int widht;
        [SerializeField] private int height;

        public EquipableContainerType ContainerType
        {
            get => containerType;
            set => containerType = value;
        }

        public string ContainerName
        {
            get => containerName;
            set => containerName = value;
        }

        public int Widht
        {
            get => widht;
            set => widht = value;
        }

        public int Height
        {
            get => height;
            set => height = value;
        }
    }
}