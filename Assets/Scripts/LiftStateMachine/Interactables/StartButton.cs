using System;
using FPSController;
using InventorySystem;
using UnityEngine;

namespace LiftStateMachine
{
    public class StartButton : Interactable
    {
        private Light _buttonLight;
        private InnerPanel _panel;
        private bool _startPressed;
        
        private void Awake()
        {
            _buttonLight = GetComponentInChildren<Light>();
            _buttonLight.enabled = false;
            _panel = gameObject.GetComponentInParent<InnerPanel>();
        }

        private void Update()
        {
            _buttonLight.enabled  = _startPressed;
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            _buttonLight.enabled = true;
            _startPressed = true;
        }

        public bool StartPressed
        {
            get => _startPressed;
            set => _startPressed = value;
        }
    }
}