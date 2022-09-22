using LiftGame.FPSController.InteractionSystem;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.LiftStateMachine.Interactables
{
    public class Button : Interactable
    {
        [SerializeField] private int buttonCommand;
        private Light _buttonLight;
        private InnerPanel _panel;

        private void Awake()
        {
            _buttonLight = GetComponentInChildren<Light>();
            _buttonLight.enabled = false;
            _panel = gameObject.GetComponentInParent<InnerPanel>();
        }

        public void TurnLightOn()
        {
            _buttonLight.enabled = true;
        }

        public void TurnLightOff()
        {
            _buttonLight.enabled = false;
        }

        public override void OnInteract(IPlayerData playerDataa)
        {
            _buttonLight.enabled = true;
            _panel.buttonPressed = true;
            _panel.Command = buttonCommand;
            _panel.CurrentSelection.Add(this);
        }


        public Light ButtonLight => _buttonLight;
        public int ButtonCommand => buttonCommand;
    }
    
}