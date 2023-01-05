using UnityEngine;

namespace LiftGame.PlayerEquipment
{
    public class ScannerWorldView : PlayerEquipmentWorldView,IPowerEquipment
    {
        public bool IsTurnedOn { get; set; }

        public override void OnUse()
        {
            Debug.Log("SCANNER USED");
            base.OnUse();
        }
        public override void OnEquip()
        {
            Debug.Log("SCANNER OnEquip");
            IsEquipped = true;
            EquipmentAnimator.SetBool(Equip,true);

        }
        public override void OnUnEquip()
        {
            Debug.Log("SCANNER OnUnEquip");
            IsEquipped = false;
            EquipmentAnimator.SetBool(UnEquip,true);
        }
        public void OnTurnOn()
        {
            Debug.Log("SCANNER TurnOn");
            IsTurnedOn = true;
        }
        public void OnTurnOff()
        {
            Debug.Log("SCANNER TurnOff");
            IsTurnedOn = false;
        }
    }
}