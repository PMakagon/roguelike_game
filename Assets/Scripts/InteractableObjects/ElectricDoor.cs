using System.Linq;
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

        private void Awake()
        {
            animator = GetComponentInParent<Animator>();
            if (!masterSwitcher) return;
            masterSwitcher.OnSwitched += ChangeLightState;
            ChangeLightState();
        }

        private void OnDestroy()
        {
            masterSwitcher.OnSwitched -= ChangeLightState;
        }

        #endregion

        #region Properties

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

        #endregion

        private void ChangeLightState()
        {
            stateLight.enabled = !masterSwitcher.IsSwitchedOn;
        }

        public override void OnInteract(IPlayerData playerData)
        {
            if (isOpen)
            {
                CloseDoor();
                return;
            }

            if (!masterSwitcher.IsSwitchedOn)
            {
                animator.SetBool(TryOpen, true);
                return;
            }

            if (!isLocked)
            {
                OpenDoor();
                return;
            }
            var inventory = playerData.GetInventoryData();
            if (CheckForKey(inventory.GetAllItems()))
            {
                OpenDoor();
            }
            else
            {
                animator.SetBool(TryOpen, true);
            }
        }
    }
}