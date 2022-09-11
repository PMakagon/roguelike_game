using UnityEngine;

namespace LiftGame.GameCore.Input.Data
{    
    [CreateAssetMenu(fileName = "InteractionInputData", menuName = "PlayerInputData/InputData")]
    public class InteractionInputData : ScriptableObject
    {
        private bool _interactedClicked;
        private bool _interactedRelease;

        public bool InteractedClicked
        {
            get => _interactedClicked;
            set => _interactedClicked = value;
        }

        public bool InteractedReleased
        {
            get => _interactedRelease;
            set => _interactedRelease = value;
        }

        public void ResetInput()
        {
            _interactedClicked = false;
            _interactedRelease = false;
        }
    }
}
