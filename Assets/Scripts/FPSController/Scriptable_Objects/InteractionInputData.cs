using UnityEngine;

namespace FPSController.Scriptable_Objects
{    
    [CreateAssetMenu(fileName = "InteractionInputData", menuName = "InteractionSystem/InputData")]
    public class InteractionInputData : ScriptableObject
    {
        private bool _interactedClicked;
        private bool _interactedRelease;
        private bool _flashlightClicked;

        public bool FlashlightClicked
        {
            get => _flashlightClicked;
            set => _flashlightClicked = value;
        }

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
