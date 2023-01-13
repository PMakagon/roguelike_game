using System.Linq;
using LiftGame.InteractableObjects.Electricals;
using LiftGame.LightingSystem;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.InteractableObjects
{
    public class ElectricDoor : InteractableDoor
    {
        [SerializeField] private MasterSwitcher masterSwitcher;
        [SerializeField] private Light stateLight;

        #region BuiltIn Methods

        protected override void Awake()
        {
            base.Awake();
            if (!masterSwitcher) return;
            masterSwitcher.OnSwitched += ChangeLightState;
            ChangeLightState();
        }

        private void OnDestroy()
        {
            masterSwitcher.OnSwitched -= ChangeLightState;
        }

        #endregion

        public MasterSwitcher MasterSwitcher
        {
            get => masterSwitcher;
            set => masterSwitcher = value;
        }
        
        public void AddSwitcher(MasterSwitcher switcher)
        {
            masterSwitcher = switcher;
            switcher.OnSwitched += ChangeLightState;
        }

        private void ChangeLightState()
        {
            stateLight.enabled = !masterSwitcher.IsSwitchedOn;
           
        }

        protected override bool ChangeDoorState()
        {
            if (IsOpen) return base.ChangeDoorState();
            if (masterSwitcher.IsSwitchedOn) return base.ChangeDoorState();
            Animator.SetBool(TryOpen, true);
            return false;
        }
    }
}