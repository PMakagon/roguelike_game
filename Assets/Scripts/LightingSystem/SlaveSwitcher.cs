using System;
using FPSController.Interaction_System;
using InventorySystem;
using UnityEngine;

namespace LightingSystem
{
    public class SlaveSwitcher : Interactable
    {
        [SerializeField] private bool isEnabled;
        [SerializeField] private Transform button;
        private Transform _buttonTransform;
        private bool _isPowered;
        private MaterialPropertyBlock _matBlock;
        private Renderer _renderer;


        private void Awake()
        {
            _buttonTransform = button;
            _renderer = button.GetComponent<Renderer>();
            _matBlock = new MaterialPropertyBlock();
            ChangeButtonPosition();
            FetchButtonEmission();
        }

        private void FetchButtonEmission()
        {
            if (!_isPowered) return;
            _renderer.GetPropertyBlock(_matBlock);
            _matBlock.SetColor("_EmissiveColor", isEnabled ? Color.clear : Color.red);
            _renderer.SetPropertyBlock(_matBlock);
        }

        private void ChangeButtonPosition()
        {
            if (isEnabled)
            {
                _buttonTransform.Rotate(-9.0f, 0.0f, 0.0f, Space.Self);
            }
            else
            {
                _buttonTransform.Rotate(9.0f, 0.0f, 0.0f, Space.Self);
            }
        }
        

        public override void OnInteract(InventoryData inventoryData)
        {
            isEnabled = !isEnabled;
            ChangeButtonPosition();
            FetchButtonEmission();
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