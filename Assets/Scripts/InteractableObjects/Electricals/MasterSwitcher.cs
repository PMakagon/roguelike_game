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

        private Action onSwitched;

        private void Start()
        {
            SetInteractionLabel();
        }

        private void SetInteractionLabel()
        {
            _toSwitch.Label = isSwitchedOn ? OFF : ON;
        }

        public Action OnSwitched
        {
            get => onSwitched;
            set => onSwitched = value;
        }


        public bool IsSwitchedOn
        {
            get => isSwitchedOn;
            set => isSwitchedOn = value;
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
            onSwitched?.Invoke();
            if (isSwitchedOn)
            {
                button.Rotate(0.0f, 0.0f, +100.0f, Space.Self);
            }
            else
            {
                button.Rotate(0.0f, 0.0f, -100.0f, Space.Self);
            }
            return true;
        }
        
    }
}
