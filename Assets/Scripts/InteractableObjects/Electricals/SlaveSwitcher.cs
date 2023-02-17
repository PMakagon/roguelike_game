using System;
using LiftGame.FPSController.InteractionSystem;
using UnityEngine;

namespace LiftGame.InteractableObjects.Electricals
{
    public class SlaveSwitcher : Interactable
    {
        [SerializeField] private MasterSwitcher masterSwitcher;
        [SerializeField] private bool isEnabled;
        [SerializeField] private Transform button;
        private Interaction _toSwitch = new Interaction("Toogle", true);
        private Transform _buttonTransform;
        private bool _isPowered = true;
        private MaterialPropertyBlock _matBlock;
        private Renderer _renderer;
        private static readonly int EmissiveColor = Shader.PropertyToID("_EmissiveColor");
        private Action<bool> _onSwitch;

        public Action<bool> OnSwitch
        {
            get => _onSwitch;
            set => _onSwitch = value;
        }

        protected override void Awake()
        {
            base.Awake();
            if (masterSwitcher)
            {
                _isPowered = masterSwitcher.IsSwitchedOn;
                masterSwitcher.OnSwitched += SetPowered;
            }
            _buttonTransform = button;
            _renderer = button.GetComponent<Renderer>();
            _matBlock = new MaterialPropertyBlock();
            FetchButtonPosition();
            FetchButtonEmission();
        }

        private void Start()
        {
            _onSwitch?.Invoke(isEnabled && _isPowered);
        }

        private void OnDestroy()
        {
            masterSwitcher.OnSwitched -= SetPowered;
        }

        public override void BindInteractions()
        {
            _toSwitch.actionOnInteract = Switch;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toSwitch);
        }

        private void FetchButtonEmission()
        {
            _renderer.GetPropertyBlock(_matBlock);
            _matBlock.SetColor(EmissiveColor, isEnabled ? Color.clear : (_isPowered ? Color.red : Color.clear));
            _renderer.SetPropertyBlock(_matBlock);
        }

        private void FetchButtonPosition()
        {
            _buttonTransform.Rotate(isEnabled? 9.0f : -9.0f , 0.0f, 0.0f, Space.Self);
        }

        private void SetPowered(bool isPowered)
        {
            _isPowered = isPowered;
            _onSwitch?.Invoke(isEnabled && _isPowered);
            FetchButtonEmission();
        }

        private bool Switch()
        {
            isEnabled = !isEnabled;
            _onSwitch?.Invoke(isEnabled && _isPowered);
            FetchButtonPosition();
            FetchButtonEmission();
            return true;
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