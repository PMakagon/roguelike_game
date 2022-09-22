using System;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.LightingSystem
{
    public class MasterSwitcher : Interactable
    {
        [SerializeField] private bool isSwitchedOn;
        [SerializeField] private Transform button;
       
        private Action onSwitched;

        public Action OnSwitched
        {
            get => onSwitched;
            set => onSwitched = value;
        }


        public bool IsSwitchedOn
        {
            get => isSwitchedOn;
            set => isSwitchedOn = value;
        }


        public override void OnInteract(IPlayerData playerData)
        {
            isSwitchedOn = !isSwitchedOn;
            onSwitched?.Invoke();
            if (isSwitchedOn)
            {
                button.Rotate(0.0f, 0.0f, +100.0f, Space.Self);
            }
            else
            {
                button.Rotate(0.0f, 0.0f, -100.0f, Space.Self);
            }
        }
    }
}
