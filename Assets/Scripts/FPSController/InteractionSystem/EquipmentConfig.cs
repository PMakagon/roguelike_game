using UnityEngine;

namespace LiftGame.FPSController.InteractionSystem
{
    [CreateAssetMenu(fileName = "EquipmentConfig", menuName = "NewInteractionSystem/EquipmentConfig")]
    public class EquipmentConfig : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private bool isPowerEquipment;

        public int ID => id;

        public bool IsPowerEquipment => isPowerEquipment;
    }
}