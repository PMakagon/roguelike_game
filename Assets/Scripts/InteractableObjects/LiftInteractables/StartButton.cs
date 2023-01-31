using LiftGame.FPSController.InteractionSystem;
using UnityEngine;

namespace LiftGame.InteractableObjects.LiftInteractables
{
    public class StartButton : Interactable
    {
        private Light _buttonLight;
        private InnerPanel _panel;
        private bool _startPressed;
        private Interaction _toPush = new Interaction("Start", true);

        protected override void Awake()
        {
            base.Awake();
            _buttonLight = GetComponentInChildren<Light>();
            _panel = gameObject.GetComponentInParent<InnerPanel>();
            _buttonLight.enabled = false;
        }

        public override void BindInteractions()
        {
            _toPush.actionOnInteract = PushStartButton;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toPush);
        }
        
        private bool PushStartButton()
        {
            _startPressed = true;
            _buttonLight.enabled = _startPressed;
            return true;
        }

        public bool StartPressed
        {
            get => _startPressed;
            set => _startPressed = value;
        }
    }
}