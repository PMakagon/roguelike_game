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
        // [SerializeField] private InventoryData _inventoryData;
        [SerializeField] private MasterSwitcher masterSwitcher;
        private Animator _animator;
        private bool _isPoweredOn;
        public bool isOpen;

        private void Awake()
        {
            _animator = GetComponentInParent<Animator>();
        }
        
        private void OpenDoor()
        {
            isOpen = true;
            _animator.SetBool("Open", isOpen);
            Debug.Log("OPEN");
        }

        private void CloseDoor()
        {
            isOpen = false;
            _animator.SetBool("Open", isOpen);
            Debug.Log("CLOSED");
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            if (!masterSwitcher.IsSwitchedOn && !isOpen)
            {
                _animator.SetBool("TryOpen", true);
                return;
            }

            if (isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
    }
}