using System;
using System.Collections;
using System.Collections.Generic;
using FPSController;
using UnityEditor;
using UnityEngine;

namespace LiftStateMachine
{
    public class Button : Interactable
    {
        [SerializeField] private int buttonCommand;
        private Light _buttonLight;
        private InnerPanel _panel;

        private void Awake()
        {
            _buttonLight = GetComponentInChildren<Light>();
            _buttonLight.enabled = false;
            _panel = gameObject.GetComponentInParent<InnerPanel>();
        }

        public void TurnLightOn()
        {
            _buttonLight.enabled = true;
        }

        public void TurnLightOff()
        {
            _buttonLight.enabled = false;
        }

        public override void OnInteract()
        {
            _buttonLight.enabled = true;
            _panel.buttonPressed = true;
            _panel.Command = buttonCommand;
            _panel.CurrentSelection.Add(this);
            base.OnInteract();
        }


        public Light ButtonLight => _buttonLight;
        public int ButtonCommand => buttonCommand;
    }
    
}