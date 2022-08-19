using LiftGame.FPSController.InteractionSystem;
using LiftGame.InventorySystem;
using UnityEngine;

namespace LiftGame.LiftStateMachine.Interactables
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
        

        public override void OnInteract(InventoryData inventoryData)
        {
            _startPressed = true;
            _buttonLight.enabled = _startPressed;
        }

        public bool StartPressed
        {
            get => _startPressed;
            set => _startPressed = value;
        }
    }
}