using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;

namespace LiftGame.PlayerEquipment
{
    public class Battery : MonoBehaviour,IPowerEquipment
    {
        private float currentCapacity = 100;
        private bool isBroken;
        
        private bool IsEmpty() => currentCapacity <= 0;
        public Animator EquipmentAnimator { get; set; }

        public bool IsEquipped { get; set; }

        public PlayerPowerData PlayerPowerData { get; set; }

        public bool IsTurnedOn { get; set; }

        public Transform EquipmentTransform 
        {
            get => gameObject.transform;
        }

        private void Awake()
        {
           
            EquipmentAnimator = GetComponent<Animator>();
        }


        public void Use()
        {
            EquipmentAnimator.SetBool("Use",true);
            if (!IsEmpty())
            {
                var charge = currentCapacity - PlayerPowerData.CurrentPower;
                PlayerPowerData.CurrentPower += charge;
                currentCapacity -= charge;
                Debug.Log("BATTERY CHARGE LEFT " + charge);
                return;
            }
            // Destroy(gameObject,5);
            Debug.Log("BATTERY EMPTY");
        }

        public void Equip()
        {
            IsEquipped = true;
            EquipmentAnimator.SetBool("Equip",true);
        }

        public void UnEquip()
        {
            IsEquipped = false;
            EquipmentAnimator.SetBool("UnEquip",true);
        }

        public void TurnOn()
        {
        }

        public void TurnOff()
        {
        }

        public float CurrentCapacity
        {
            get => currentCapacity;
            set => currentCapacity = value;
        }
    }
}