using System;
using FPSController;
using FPSController.Interaction_System;
using InventorySystem;
using LevelGeneration;
using NaughtyAttributes;
using UnityEngine;
using VHS;

namespace InteractableObjects
{
    public class InteractableDoor : Interactable
    {
        [SerializeField] private bool isLocked;
        [ShowIf("isLocked")] [SerializeField] private string keyName;
        private Animator _animator;
        public bool isOpen;

        private void Awake()
        {
            // _animator = GetComponentInChildren<Animator>();
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
            if (!isOpen && isLocked)
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