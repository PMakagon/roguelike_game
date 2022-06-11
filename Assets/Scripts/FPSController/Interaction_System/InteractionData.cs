using InventorySystem;
using UnityEngine;

namespace FPSController.Interaction_System
{    
    [CreateAssetMenu(fileName = "Interaction Data", menuName = "InteractionSystem/InteractionData")]
    public class InteractionData : ScriptableObject
    {
        private Interactable _interactable;

        public Interactable Interactable
        {
            get => _interactable;
            set => _interactable = value;
        }

        public void Interact(InventoryData inventoryData)
        {
            _interactable.OnInteract(inventoryData);
            ResetData();
        }

        public bool IsSameInteractable(Interactable newInteractable) => _interactable == newInteractable;
        public bool IsEmpty() => _interactable == null;
        public void ResetData() => _interactable = null;

    }
}
