using FPSController;
using FPSController.Interaction_System;
using InventorySystem;
using UnityEngine;

namespace LiftStateMachine
{
    public class StopButton : Interactable
    {
        private Light _buttonLight;
        private InnerPanel _panel;
        private bool _stopPressed;
        
        private void Awake()
        {
            _buttonLight = GetComponentInChildren<Light>();
            _buttonLight.enabled = false;
            _panel = gameObject.GetComponentInParent<InnerPanel>();
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            _stopPressed = !_stopPressed;
            _buttonLight.enabled = _stopPressed;
        }

        public bool StopPressed
        {
            get => _stopPressed;
            set => _stopPressed = value;
        }
    }
}