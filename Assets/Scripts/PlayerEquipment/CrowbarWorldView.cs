using UnityEngine;

namespace LiftGame.PlayerEquipment
{
    public class CrowbarWorldView : PlayerEquipmentWorldView
    {
        public override void OnUse()
        {
            Debug.Log("CROWBAR USED");
            base.OnUse();
        }
        public override void OnEquip()
        {
            Debug.Log("CROWBAR OnEquip");
            base.OnEquip();

        }
        public override void OnUnEquip()
        {
            Debug.Log("CROWBAR OnUnEquip");
            base.OnUnEquip();
        }
    }
}