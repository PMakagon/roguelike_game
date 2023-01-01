using System.Collections.Generic;
using System.Linq;
using FarrokGames.Inventory.Runtime;
using FarrokhGames.Inventory;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.NewInventory.Items;
using LiftGame.PlayerCore;
using ModestTree;
using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.InteractableObjects
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
        protected static readonly int ForceClose = Animator.StringToHash("ForceClose");

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

        public void ForceCloseDoor()
        {
            if (!isOpen) return;
            isOpen = false;
            animator.SetBool(ForceClose, isOpen);
            SetToolTip();
        }

        protected void SetToolTip()
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

        protected bool CheckForKey(List<IInventoryItem> allItems)
        {
            if (allItems.IsEmpty()) return false;
            foreach (var item in allItems)
            {
                if (item==null) continue;
                if ((item.GetType() != typeof(Key))) continue;
                if ((item as Key)?.KeyCode == keyName)
                {
                    isLocked = false;
                    // Debug.Log("Opened with " + key.Name);
                    return true;
                }
            }
            return false;
        }

        public override void OnInteract(IPlayerData playerData)
        {
            if (isOpen)
            {
                CloseDoor();
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