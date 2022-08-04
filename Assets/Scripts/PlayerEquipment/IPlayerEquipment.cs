using UnityEngine;

namespace PlayerEquipment
{
    public interface IPlayerEquipment

    {
        Transform EquipmentTransform { get; }
        Animator EquipmentAnimator { get; set; }
        bool IsEquipped { get; set; }
        void Use();
        void Equip();
        void UnEquip();
    }
}