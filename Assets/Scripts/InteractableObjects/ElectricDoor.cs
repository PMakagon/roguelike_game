using LiftGame.InteractableObjects.Electricals;
using UnityEngine;

namespace LiftGame.InteractableObjects
{
    public class ElectricDoor : InteractableDoor
    {
        [SerializeField] private MasterSwitcher masterSwitcher;
        [SerializeField] private Light stateLight;
        

        protected override void Awake()
        {
            base.Awake();
            if (!masterSwitcher) return;
            masterSwitcher.OnSwitched += ChangeLightState;
            ChangeLightState(masterSwitcher.IsSwitchedOn);
        }

        private void OnDestroy()
        {
            if (!masterSwitcher) return;
            masterSwitcher.OnSwitched -= ChangeLightState;
        }

        public void AddSwitcher(MasterSwitcher switcher)
        {
            masterSwitcher = switcher;
            switcher.OnSwitched += ChangeLightState;
        }

        private void ChangeLightState(bool state)
        {
            stateLight.enabled = !state;
        }

        protected override bool ChangeDoorState()
        {
            if (IsOpen) return base.ChangeDoorState();
            if (masterSwitcher.IsSwitchedOn) return base.ChangeDoorState();
            Animator.SetBool(TryOpen, true);
            return false;
        }

        public MasterSwitcher MasterSwitcher
        {
            get => masterSwitcher;
            set => masterSwitcher = value;
        }
    }
}