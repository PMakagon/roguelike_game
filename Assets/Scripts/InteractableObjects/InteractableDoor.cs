using System;
using FPSController;
using LevelGeneration;
using NaughtyAttributes;
using UnityEngine;

namespace InteractableObjects
{
    public class InteractableDoor : InteractableBase
    {
        [SerializeField] private GameObject door;
        [SerializeField] private bool isLocked;
        [ShowIf("isLocked")] [SerializeField] private string keyName;
        [SerializeField] private InventoryData _inventoryData;
        private Animator _animator;
        private MConnector _doorConnector;
        public bool isOpen;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _doorConnector = GetComponentInChildren<MConnector>();
        }

        public MConnector DoorConnector => _doorConnector;

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