using LiftGame.InventorySystem;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.FPSController.InteractionSystem
{    
    [CreateAssetMenu(fileName = "Interaction Data", menuName = "Player/InteractionSystem/InteractionData")]
    public class InteractionData : ScriptableObject
    {
        private IInteractable _interactable;

        public IInteractable Interactable
        {
            get => _interactable;
            set => _interactable = value;
        }

        public void Interact(IPlayerData playerData)
        {
            _interactable.OnInteract(playerData);
            ResetData();
        }

        public bool IsSameInteractable(IInteractable newInteractable) => _interactable == newInteractable;
        public bool IsEmpty() => _interactable == null;
        public void ResetData() => _interactable = null;

    }
}
