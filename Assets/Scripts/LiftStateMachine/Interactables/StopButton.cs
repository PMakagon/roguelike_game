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

        private void Update()
        {
            if (!_stopPressed)
            {
                _buttonLight.enabled = false;
            }
        }
        

        public override void OnInteract(InventoryData inventoryData)
        {
            _buttonLight.enabled = true;
            _stopPressed = !_stopPressed;
        }

        public bool StopPressed
        {
            get => _stopPressed;
            set => _stopPressed = value;
        }
    }
}