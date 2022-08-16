using System;
using System.Linq;
using FPSController;
using FPSController.Interaction_System;
using InventorySystem;
using LightingSystem;
using NaughtyAttributes;
using UnityEngine;

namespace InteractableObjects
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

        public override void OnInteract(InventoryData inventoryData)
        {
            if (!isOpen)
            {
                if (masterSwitcher.IsSwitchedOn)
                {
                    if (isLocked)
                    {
                        if (inventoryData.Items.Any(key => key.Name == keyName))
                        {
                            isLocked = false;
                            OpenDoor();
                            // Debug.Log("Opened with " + key.Name);
                            return;
                        }
                        animator.SetBool(TryOpen, true);
                        return;
                    }
                    OpenDoor();
                    return;
                }
                animator.SetBool(TryOpen, true);
                return;
            }
            CloseDoor();
        }
    }
}