using System;
using UnityEditor;
using UnityEngine;

namespace LiftStateMachine.Interactables
{
    public class Button : PanelButton
    {
        private InnerPanel _panel;
        [SerializeField]  private int ButtonCommand;


        private void Awake()
        {
            _panel = gameObject.GetComponentInParent<InnerPanel>();
        }

        public void OnButtonInteract()
        {
            _panel.buttonPressed = true;
            _panel._command = ButtonCommand;
        }
    }
    
    
}