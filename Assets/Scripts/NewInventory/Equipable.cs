using LiftGame.FPSController.InteractionSystem;
using LiftGame.NewInventory.Items;
using LiftGame.PlayerCore;
using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.NewInventory
{
    public class Equipable : Interactable
    {
        [Space, Header("Equipable Settings")] 
        [SerializeField] private bool isEquipable = true;
        [ShowIf("isEquipable")] [SerializeField] public ItemDefinition itemToEquip;
        [ShowIf("isEquipable")] [SerializeField] private int amount;
        [ShowIf("isEquipable")] [SerializeField] private bool destroyOnEquip = true;
        
        public bool IsEquipable => isEquipable;
        public int Amount => amount;
        public bool DestroyOnEquip => destroyOnEquip;

        private void Awake()
        {
            TooltipMessage = itemToEquip.Name;
        }

        public override void OnInteract(IPlayerData playerData)
        {
            var inventory=  playerData.GetInventoryData();
            if (inventory.TryToAddItem(itemToEquip));
            {
                if (destroyOnEquip)
                {
                    Destroy(gameObject);
                }
                Debug.Log("Equipped: " + gameObject.name);
            }
            base.OnInteract(playerData);
        }
    }
}