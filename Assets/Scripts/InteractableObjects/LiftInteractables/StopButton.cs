using LiftGame.FPSController.InteractionSystem;
using UnityEngine;

namespace LiftGame.InteractableObjects.LiftInteractables
{
    public class StopButton : Interactable
    {
        private Light _buttonLight;
        private InnerPanel _panel;
        private bool _stopPressed;
        private Interaction _toPush = new Interaction("Stop", true);

        protected override void Awake()
        {
            base.Awake();
            _buttonLight = GetComponentInChildren<Light>();
            _panel = gameObject.GetComponentInParent<InnerPanel>();
            _buttonLight.enabled = false;
        }

        public override void BindInteractions()
        {
            _toPush.actionOnInteract = PushStopButton;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toPush);
        }
        
        private bool PushStopButton()
        {
            _stopPressed = !_stopPressed;
            _buttonLight.enabled = _stopPressed;
            return true;
        }

        public bool StopPressed
        {
            get => _stopPressed;
            set => _stopPressed = value;
        }
    }
}