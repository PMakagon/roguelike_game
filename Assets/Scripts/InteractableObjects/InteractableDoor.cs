using System.Collections.Generic;
using LiftGame.FPSController.InteractionSystem;
using LiftGame.Inventory.Core;
using LiftGame.Inventory.Items;
using ModestTree;
using NaughtyAttributes;
using UnityEngine;

namespace LiftGame.InteractableObjects
{
    public class InteractableDoor : Interactable
    {
        [SerializeField] private string doorName = "Door";
        [SerializeField] protected bool isLocked;
        [SerializeField] private float timeToUnlock = 1;
        [ShowIf("isLocked")] [SerializeField] protected string keyCode;

        [Header("INTERACTIONS")] [SerializeField]
        private InteractionConfig breachConfig = null;

        protected Animator Animator;
        protected bool IsOpen;
        protected bool IsExamined;

        private Interaction _toOpen;
        private Interaction _toUnlock;
        private Interaction _toBreach;

        protected static readonly int Open = Animator.StringToHash("Open");
        protected static readonly int TryOpen = Animator.StringToHash("TryOpen");
        protected static readonly int ForceOpen = Animator.StringToHash("ForceOpen");
        protected static readonly int ForceClose = Animator.StringToHash("ForceClose");

        #region BuiltIn Methods

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponentInParent<Animator>();
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
            get => keyCode;
            set => keyCode = value;
        }

        #endregion

        protected void SetToolTip()
        {
            TooltipMessage = doorName+ "\n" + (isLocked ? "" :  KeyName); 
        }

        public override void CreateInteractions()
        {
            _toOpen = new Interaction("Open", true);
            _toUnlock = new Interaction("Unlock",timeToUnlock, false);
            _toBreach = new Interaction(breachConfig, false);
        }

        public override void BindInteractions()
        {
            _toOpen.ActionOnInteract = ChangeDoorState;
            _toUnlock.ActionOnInteract = Unlock;
            _toBreach.ActionOnInteract = Breach;
        }

        public override void AddInteractions()
        {
            Interactions.Add(_toOpen);
            Interactions.Add(_toUnlock);
            Interactions.Add(_toBreach);
            _toOpen.IsEnabled = true;
        }

        public void ForceOpenDoor()
        {
            if (!IsExamined && isLocked) return;
            isLocked = false;
            IsOpen = true;
            Animator.SetBool(ForceOpen, IsOpen);
            SetToolTip();
        }

        public void ForceCloseDoor()
        {
            if (!IsOpen) return;
            IsOpen = false;
            Animator.SetBool(ForceClose, IsOpen);
            SetToolTip();
        }

        protected void OpenDoor()
        {
            IsOpen = true;
            _toOpen.Label = "Close";
            Animator.SetBool(Open, IsOpen);
        }

        protected void CloseDoor()
        {
            IsOpen = false;
            _toOpen.Label = "Open";
            Animator.SetBool(Open, IsOpen);
        }

        protected bool TryUnlock(List<IInventoryItem> allItems)
        {
            if (allItems.IsEmpty()) return false;
            foreach (var item in allItems)
            {
                if (item == null) continue;
                if ((item.GetType() != typeof(Key))) continue;
                if ((item as Key)?.KeyCode == keyCode)
                {
                    isLocked = false;
                    // Debug.Log("Opened with " + key.Name);
                    return true;
                }
            }

            return false;
        }

        public override void OnInteract(Interaction interaction)
        {
            IsExamined = true;
            base.OnInteract(interaction);
        }

        protected bool Breach()
        {
            ForceOpenDoor();
            _toOpen.IsEnabled = true;
            _toUnlock.IsEnabled = false;
            _toBreach.IsEnabled = false;
            return true;
        }

        protected bool Unlock()
        {
            var inventory = CachedPlayerData.GetInventoryData();
            if (TryUnlock(inventory.GetAllItems()))
            {
                Animator.SetBool(TryOpen, true);
                _toOpen.IsEnabled = true;
                _toUnlock.IsEnabled = false;
                _toBreach.IsEnabled = false;
                return true;
            }

            return false;
        }

        protected virtual bool ChangeDoorState()
        {
            if (isLocked)
            {
                Animator.SetBool(TryOpen, true);
                _toOpen.IsEnabled = false;
                _toUnlock.IsEnabled = true;
                _toBreach.IsEnabled = true;
                return false;
            }

            if (IsOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }

            return true;
        }
    }
}