using UnityEngine;

namespace LiftGame.PlayerEquipment
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