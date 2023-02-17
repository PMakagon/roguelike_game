using LiftGame.FPSController.InteractionSystem;
using UnityEngine;

namespace LiftGame.PlayerEquipment
{
    [RequireComponent(typeof(Animator))]
    public abstract class PlayerEquipmentWorldView : MonoBehaviour
    {
        [SerializeField] private EquipmentConfig equipmentConfig;
        private EquipmentData _equipmentData;
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

        public EquipmentConfig EquipmentConfig => equipmentConfig;

        public EquipmentData EquipmentData
        {
            get { return _equipmentData ??= new EquipmentData(); }
            set => _equipmentData = value;
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