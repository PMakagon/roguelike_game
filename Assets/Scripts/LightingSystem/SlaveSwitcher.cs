using System;
using FPSController;
using FPSController.Interaction_System;
using InventorySystem;
using UnityEngine;

namespace LightingSystem
{
    public class SlaveSwitcher : Interactable
    {
        [SerializeField] private bool isEnabled;
        [SerializeField] private GameObject button;
        private Transform _buttonTransform;
        private Material _material;
        private bool _isPowered;

        private void Awake()
        {
            _buttonTransform = button.transform;
            _material = button.GetComponent<Renderer>().material;
            if (isEnabled)
            {
                ClickOn();
                return;
            }
            ClickOff();
        }

        private void ClickOn()
        {
            if (_isPowered)
            {
                _material.SetColor("_EmissiveColor",Color.clear);
            }
            _buttonTransform.Rotate(-9.0f, 0.0f, 0.0f, Space.Self);
        }

        private void ClickOff()
        {
            if (_isPowered)
            {
                _material.SetColor("_EmissiveColor",Color.red);
            }
            _buttonTransform.Rotate(9.0f, 0.0f, 0.0f, Space.Self);
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            isEnabled = !isEnabled;
            if (isEnabled)
            {
                ClickOn();
                return;
            }
            ClickOff();
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set => isEnabled = value;
        }

        public bool IsPowered
        {
            get => _isPowered;
            set => _isPowered = value;
        }
    }
}