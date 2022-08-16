using System.Linq;
using FPSController.Interaction_System;
using InventorySystem;
using NaughtyAttributes;
using UnityEngine;

namespace InteractableObjects
{
    public class InteractableDoor : Interactable
    {
        [SerializeField] protected bool isLocked;
        [ShowIf("isLocked")] [SerializeField] protected string keyName;
        protected Animator animator;
        protected bool isOpen;

        protected static readonly int Open = Animator.StringToHash("Open");
        protected static readonly int TryOpen = Animator.StringToHash("TryOpen");
        protected static readonly int ForceOpen = Animator.StringToHash("ForceOpen");
        
        #region BuiltIn Methods
        private void Awake()
        {
            animator = GetComponentInParent<Animator>();
            SetToolTip();
        }
        #endregion
        
        #region Properties

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

        #endregion
        
        public void ForceOpenDoor()
        {
            if (isLocked) return;
            isOpen = true;
            animator.SetBool(ForceOpen, isOpen);
            SetToolTip();
        }

        private void SetToolTip()
        {
            TooltipMessage = isOpen ? "Close" : "Open";
        }

        public void ChangeState()
        {
            if (isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }

        protected void OpenDoor()
        {
            isOpen = true;
            animator.SetBool(Open, isOpen);
            SetToolTip();
        }

        protected void CloseDoor()
        {
            isOpen = false;
            animator.SetBool(Open, isOpen);
            SetToolTip();
        }

        public override void OnInteract(InventoryData inventoryData)
        {
            if (!isOpen)
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
            }
            else
            {
                CloseDoor();
            }
        }

    }
}