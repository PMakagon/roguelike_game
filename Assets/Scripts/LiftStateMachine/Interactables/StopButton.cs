using FPSController;
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
        

        public override void OnInteract()
        {
            _buttonLight.enabled = true;
            _stopPressed = !_stopPressed;
            base.OnInteract();
            // Debug.Log("Start pressed");
        }

        public bool StopPressed
        {
            get => _stopPressed;
            set => _stopPressed = value;
        }
    }
}