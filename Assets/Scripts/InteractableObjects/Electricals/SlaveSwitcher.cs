using LiftGame.FPSController.InteractionSystem;
using UnityEngine;

namespace LiftGame.InteractableObjects.Electricals
{
    public class SlaveSwitcher : Interactable
    {
        [SerializeField] private bool isEnabled;
        [SerializeField] private Transform button;
        private Interaction _toSwitch = new Interaction("Toogle", true);
        private Transform _buttonTransform;
        private bool _isPowered;
        private MaterialPropertyBlock _matBlock;
        private Renderer _renderer;
        private static readonly int EmissiveColor = Shader.PropertyToID("_EmissiveColor");

        protected override void Awake()
        {
            base.Awake();
            _buttonTransform = button;
            _renderer = button.GetComponent<Renderer>();
            _matBlock = new MaterialPropertyBlock();
            ChangeButtonPosition();
            FetchButtonEmission();
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

        private bool Switch()
        {
            isEnabled = !isEnabled;
            ChangeButtonPosition();
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