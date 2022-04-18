using System;
using FPSController;
using UnityEditor;
using UnityEngine;

namespace LiftStateMachine
{
    public class Button : Interactable
    {
        [SerializeField]  private int buttonCommand;
        private InnerPanel _panel;
        private Light _buttonLight;
        // [SerializeField] private Animation flashAnimation;
        // [SerializeField] private Animation pressAnimation;


        private void Awake()
        {
            _buttonLight = GetComponentInChildren<Light>();
            _buttonLight.enabled = false;
            // flashAnimation = GetComponent<Animation>();
            _panel = gameObject.GetComponentInParent<InnerPanel>();
        }

        public int ButtonCommand => buttonCommand;

        public void FlashIncorrect()
        {
            // flashAnimation.Play();
            _buttonLight.enabled = false;

        } 
        public void FlashCorrect()
        {
            // flashAnimation.Play();
            _buttonLight.enabled = false;

        }

        private void LightOff()
        {
            _buttonLight.enabled = false;
        }

        public override void OnInteract()
        {
            // pressAnimation.Play();
            _buttonLight.enabled = true;
            _panel.buttonPressed = true;
            _panel._command = buttonCommand;
            base.OnInteract();
        }
    }
    
    
}