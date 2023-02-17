using LiftGame.Inventory.Items;
using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.FPSController.InteractionSystem
{
    public enum InteractionRequirement
    {
        None,
        InventoryItem,
        Equipment
    }

    [CreateAssetMenu(fileName = "Interaction", menuName = "NewInteractionSystem/Interaction")]
    public class InteractionConfig : ScriptableObject
    {
        [SerializeField] private string label = "Interact";
        [SerializeField] private InteractionRequirement requirement = InteractionRequirement.None;
        [SerializeField] private bool isHoldInteract;

        [ShowIf("isHoldInteract")] 
        [SerializeField] private float holdDuration = 1f;

        [ShowIf("requirement", InteractionRequirement.Equipment)] 
        [BoxGroup("EQ")] [SerializeField] private EquipmentConfig equipmentConfig;
        [ShowIf("requirement", InteractionRequirement.Equipment)] 
        [BoxGroup("EQ")] [SerializeField] private bool isShouldBeTurnedOn;
        [ShowIf("requirement", InteractionRequirement.Equipment)] 
        [BoxGroup("EQ")] [SerializeField] private bool isEquipmentUseNeeded;

        
        [ShowIf("requirement", InteractionRequirement.InventoryItem)] 
        [BoxGroup("Item")] [SerializeField] private ItemDefinition itemDefinition;
        [ShowIf("requirement", InteractionRequirement.InventoryItem)] 
        [BoxGroup("Item")] [SerializeField] private bool removeOnInteract;

        public string Label => label;
        public InteractionRequirement Requirement => requirement;
        public bool IsHoldInteract => isHoldInteract;
        public float HoldDuration => holdDuration;
        public EquipmentConfig EquipmentConfig => equipmentConfig;
        public bool IsShouldBeTurnedOn => isShouldBeTurnedOn;
        public bool IsEquipmentUseNeeded => isEquipmentUseNeeded;
        public ItemDefinition ItemDefinition => itemDefinition;
        public bool RemoveOnInteract => removeOnInteract;
    }
}