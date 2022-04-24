using System;
using FPSController;
using NaughtyAttributes;
using UnityEngine;

namespace InteractableObjects
{
    public class InteractableDoor : InteractableBase
    {
        [SerializeField] private GameObject door;
        public Animator animator;
        private Rigidbody doorRigidbody;
        [SerializeField] private bool isLocked;
        [ShowIf("isLocked")] [SerializeField] private string keyName;
        public bool isOpen;
        [SerializeField] private InventoryData _inventoryData;

        private void Start()
        {
            // doorRigidbody = door.GetComponent<Rigidbody>();
            // doorRigidbody.isKinematic = true;
            // animator = GetComponent<Animator>();

            if (TryGetComponent(typeof(Animator), out Component component))
            {
                animator = component.GetComponent<Animator>();
            }
            
        }

        private void OpenDoor()
        {
            isOpen = true;
            animator.SetBool("Open", isOpen);
            // doorRigidbody.isKinematic = false;
            Debug.Log("OPEN");

        }

        private void CloseDoor()
        {
            isOpen = false;
            animator.SetBool("Open", isOpen);
            // doorRigidbody.isKinematic = true;
            Debug.Log("CLOSED");
        }

        public override void OnInteract()
        {
            if (!isOpen && isLocked)
            {
                foreach (var key in _inventoryData.Items)
                {
                    if (key.Name == keyName)
                    {
                        isLocked = false;
                        OpenDoor();
                        Debug.Log("Opened with " + key.Name);
                        return;
                    }
                }
                animator.SetBool("TryOpen",true);
                return;
            }
            if(isOpen)
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