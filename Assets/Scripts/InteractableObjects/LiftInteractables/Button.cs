using LiftGame.FPSController.InteractionSystem;
using UnityEngine;

namespace LiftGame.InteractableObjects.LiftInteractables
{
    public class Button : Interactable
    {
        [SerializeField] private int buttonCommand;
        private Light _buttonLight;
        private InnerPanel _panel;
        private Interaction _toPush = new Interaction("Push", true);

        protected override void Awake()
        {
            base.Awake();
            _buttonLight = GetComponentInChildren<Light>();
            _panel = gameObject.GetComponentInParent<InnerPanel>();
            _buttonLight.enabled = false;
        }

        public override void BindInteractions()
        {
            _toPush.actionOnInteract = PushButton;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toPush);
        }

        public void TurnLightOn()
        {
            _buttonLight.enabled = true;
        }

        public void TurnLightOff()
        {
            _buttonLight.enabled = false;
        }

        private bool PushButton()
        {
            _buttonLight.enabled = true;
            _panel.buttonPressed = true;
            _panel.Command = buttonCommand;
            _panel.CurrentSelection.Add(this);
            return true;
        }

        public Light ButtonLight => _buttonLight;
        public int ButtonCommand => buttonCommand;
    }
}