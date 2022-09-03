using LiftGame.InventorySystem;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.FPSController.InteractionSystem
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

        public void Interact(IPlayerData playerData)
        {
            _interactable.OnInteract(playerData);
            ResetData();
        }

        public bool IsSameInteractable(Interactable newInteractable) => _interactable == newInteractable;
        public bool IsEmpty() => _interactable == null;
        public void ResetData() => _interactable = null;

    }
}
