using System;
using FPSController;
using FPSController.Interaction_System;
using InventorySystem;
using LightingSystem;
using NaughtyAttributes;
using UnityEngine;

namespace InteractableObjects
{
    public class ElectricDoor : Interactable
    {
        [SerializeField] private bool isLocked;
        [ShowIf("isLocked")] [SerializeField] private string keyName;
        [SerializeField] private MasterSwitcher masterSwitcher;
        [SerializeField] private Light stateLight;
        private Animator _animator;
        private bool _isOpen;

        private void Awake()
        {
            _animator = GetComponentInParent<Animator>();
            stateLight.enabled = !masterSwitcher.IsSwitchedOn;
        }

        public MasterSwitcher MasterSwitcher
        {
            get => masterSwitcher;
            set => masterSwitcher = value;
        }

        private void Update()
        {
            if (!masterSwitcher.IsSwitchedOn)
            {
                stateLight.enabled = true;
            }
            else
            {
                stateLight.enabled = false;
            }
        }

        public void ChangeState()
        {
            if (_isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
        
        private void OpenDoor()
        {
            _isOpen = true;
            _animator.SetBool("Open", _isOpen);
            // Debug.Log("OPEN");
        }

        private void CloseDoor()
        {
            _isOpen = false;
            _animator.SetBool("Open", _isOpen);
            // Debug.Log("CLOSED");
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            if (!_isOpen)
            {
                if (masterSwitcher.IsSwitchedOn)
                {
                    if (isLocked)
                    {
                        foreach (var key in inventoryData.Items)
                        {
                            if (key.Name == keyName)
                            {
                                isLocked = false;
                                OpenDoor();
                                Debug.Log("Opened with " + key.Name);
                                return;
                            }
                        }
                        _animator.SetBool("TryOpen", true);
                        return;
                    }
                    OpenDoor();
                    return;
                }
                _animator.SetBool("TryOpen", true);
                return;
            }
            CloseDoor();
        }
    }
}