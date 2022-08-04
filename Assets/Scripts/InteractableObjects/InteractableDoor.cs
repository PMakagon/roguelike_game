using FPSController.Interaction_System;
using InventorySystem;
using NaughtyAttributes;
using UnityEngine;

namespace InteractableObjects
{
    public class InteractableDoor : Interactable
    {
        [SerializeField] private bool isLocked;
        [ShowIf("isLocked")] [SerializeField] private string keyName;
        private Animator _animator;
        private bool _isOpen;

        private void Awake()
        {
            // _animator = GetComponentInChildren<Animator>();
            _animator = GetComponentInParent<Animator>();
            SetToolTip();
        }

        private void SetToolTip()
        {
            if (_isOpen)
            {
                TooltipMessage = "Close";
            }
            else
            {
                TooltipMessage = "Open";
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
           SetToolTip();
            // Debug.Log("OPEN");
        }

        private void CloseDoor()
        {
            _isOpen = false;
            _animator.SetBool("Open", _isOpen);
            SetToolTip();
            // Debug.Log("CLOSED");
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            if (!_isOpen)
            {
                if (isLocked)
                {
                    foreach (var key in inventoryData.Items)
                    {
                        if (key.Name == keyName)
                        {
                            isLocked = false;
                            OpenDoor();
                            // Debug.Log("Opened with " + key.Name);
                            return;
                        }
                    }
                    _animator.SetBool("TryOpen", true);
                    return;
                }
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }

        public bool IsLocked
        {
            get => isLocked;
            set => isLocked = value;
        }

        public string KeyName
        {
            get => keyName;
            set => keyName = value;
        }
    }
}