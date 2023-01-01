using System;
using UnityEngine;

namespace LiftGame.PlayerEquipment
{
    [RequireComponent(typeof(Animator))]
    public abstract class PlayerEquipmentWorldView : MonoBehaviour
    {
        private Animator equipmentAnimator;
        protected static readonly int Use = Animator.StringToHash("Use");
        protected static readonly int Equip = Animator.StringToHash("Equip");
        protected static readonly int UnEquip = Animator.StringToHash("UnEquip");
        protected static readonly int TurnOn = Animator.StringToHash("TurnOn");
        protected static readonly int TurnOff = Animator.StringToHash("TurnOff");

        private void Awake()
        {
            equipmentAnimator = GetComponent<Animator>();
        }

        public Animator EquipmentAnimator
        {
            get => equipmentAnimator;
            set => equipmentAnimator = value;
        }

        public bool IsEquipped { get; set; }

        public virtual void OnUse()
        { 
            EquipmentAnimator.SetBool(Use,true);
        }

        public virtual void OnEquip()
        {
            IsEquipped = true;
            EquipmentAnimator.SetBool(Equip,true);
        }

        public virtual void OnUnEquip()
        {
            IsEquipped = false;
            EquipmentAnimator.SetBool(UnEquip,true);
        }
    }
}