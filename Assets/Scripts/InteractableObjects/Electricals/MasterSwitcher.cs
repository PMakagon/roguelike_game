using System;
using LiftGame.FPSController.InteractionSystem;
using UnityEngine;

namespace LiftGame.InteractableObjects.Electricals
{
    public class MasterSwitcher : Interactable
    {
        [SerializeField] private bool isSwitchedOn;
        [SerializeField] private Transform button;
        private Interaction _toSwitch = new Interaction("Toogle", true);
        private const string OFF = "Turn OFF";
        private const string ON = "Turn ON";

        private Action<bool> _onSwitched;

        private void Start()
        {
            SetInteractionLabel();
            _onSwitched?.Invoke(isSwitchedOn);
        }

        private void SetInteractionLabel()
        {
            _toSwitch.Label = isSwitchedOn ? OFF : ON;
        }

        public override void BindInteractions()
        {
            _toSwitch.actionOnInteract = Switch;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toSwitch);
        }
        

        private bool Switch()
        {
            isSwitchedOn = !isSwitchedOn;
            SetInteractionLabel();
            _onSwitched?.Invoke(isSwitchedOn);
            button.Rotate(0.0f, 0.0f, isSwitchedOn? -100.0f : +100.0f, Space.Self);
            return true;
        }

        public Action<bool> OnSwitched
        {
            get => _onSwitched;
            set => _onSwitched = value;
        }

        public bool IsSwitchedOn
        {
            get => isSwitchedOn;
            set => isSwitchedOn = value;
        }
    }
}
