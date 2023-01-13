using LiftGame.FPSController.InteractionSystem;
using LiftGame.Inventory.Items;
using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.Inventory
{
    public class Equipable : Interactable
    {
        [Space, Header("Equipable Settings")] 
        [SerializeField] private bool isEquipable = true;
        [ShowIf("isEquipable")] 
        [SerializeField] public ItemDefinition itemToEquip;
        [ShowIf("isEquipable")] 
        [SerializeField] private int amount;
        [ShowIf("isEquipable")] 
        [SerializeField] private bool destroyOnEquip = true;

        private Interaction _toEquip;
        
        public bool IsEquipable => isEquipable;
        public int Amount => amount;
        public bool DestroyOnEquip => destroyOnEquip;

        protected override void Awake()
        {
            base.Awake();
            TooltipMessage = ((itemToEquip==null)? gameObject.name : itemToEquip.Name);
        }

        public override void CreateInteractions()
        {
            _toEquip = new Interaction("Equip",true);
        }

        public override void BindInteractions()
        {
            _toEquip.actionOnInteract = Equip;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toEquip);
        }

        private bool Equip()
        {
            if (!isEquipable) return false;
            var inventory = CachedPlayerData.GetInventoryData();
            if (inventory.TryToAddItem(itemToEquip))
            {
                if (destroyOnEquip) Destroy(gameObject);
                Debug.Log("Equipped: " + gameObject.name);
                return true;
            }
            return false;
        }
        
        public override void OnInteract(Interaction interaction)
        {
            
        }
    }
}