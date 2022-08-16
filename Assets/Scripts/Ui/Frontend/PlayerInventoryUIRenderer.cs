using System;
using System.Collections.Generic;
using FPSController.Camera_Controller;
using InventorySystem.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace InventorySystem
{
    public class MouseContainer
    {
        public InventorySlot pointSlot;
        public InventorySlot hoverSlot;
        public IItem pointItem;
        public IItem hoverItem;

        public void SetSlot(InventorySlot slotToSet)
        {
            if (slotToSet == null)
            {
                pointSlot = null;
                pointItem = null;
                return;
            }

            pointSlot = slotToSet;
            if (slotToSet.ItemInSlot != null)
            {
                pointItem = slotToSet.ItemInSlot;
            }
        }

        public void SetHoverSlot(InventorySlot slotToSet)
        {
            if (slotToSet == null)
            {
                hoverSlot = null;
                hoverItem = null;
                return;
            }

            hoverSlot = slotToSet;
            if (slotToSet.ItemInSlot != null)
            {
                hoverItem = slotToSet.ItemInSlot;
            }
        }
        
    }

    public class PlayerInventoryUIRenderer : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private InventoryData inventoryData;
        [SerializeField] private InventorySlot inventorySlotUITemplate;
        [SerializeField] private GameObject inventoryWindow;
        [SerializeField] private Transform playerInventoryPanel;
        [SerializeField] private GameObject containerWindow;
        [SerializeField] private TextMeshProUGUI containerName;
        [SerializeField] private Transform containerPanel;
        [SerializeField] private Transform equipmentPanel;
        [SerializeField] private List<InventorySlot> inventorySlotsDisplayed;
        [SerializeField] private List<InventorySlot> containerSlotsDisplayed;
        [SerializeField] private EquipmentItem[] equipmentSlots = new EquipmentItem[4];

        private bool _isWindowActive;
        private MouseContainer _mouseContainer = new MouseContainer();

        void Start()
        {
            containerSlotsDisplayed = new List<InventorySlot>();
            inventoryData.ResetData();
            inventorySlotsDisplayed = new List<InventorySlot>(inventoryData.Items.Capacity);
            // inventoryData.onItemAdd += UpdateInventory;
            inventoryData.onContainerOpen += OpenContainerPanel;
            RenderInventory();
        }

        void Update()
        {
            HandleInput();
        }


        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _isWindowActive = !_isWindowActive;
                inventoryWindow.SetActive(_isWindowActive);
                cameraController.LockCursor(!_isWindowActive);
                // cameraController.IsCameraFreezed = _isWindowActive;
                if (_isWindowActive)
                {
                    UpdateInventory();
                }
                else
                {
                    CloseContainerPanel();
                }
            }
        }

        private void OpenContainerPanel()
        {
            _isWindowActive = true;
            inventoryWindow.SetActive(true);
            containerWindow.SetActive(true);
            cameraController.LockCursor(!_isWindowActive);
            // cameraController.IsCameraFreezed = _isWindowActive;
            containerName.text = inventoryData.ContainerName;
            RenderContainer();
        }

        private void CloseContainerPanel()
        {
            containerWindow.SetActive(false);
            inventoryData.ClearContainer();
        }

        private InventorySlot CreateNewSlot(Transform parentPanel)
        {
            var cell = Instantiate(inventorySlotUITemplate, parentPanel);
            cell.ParentPanel = parentPanel;
            AddEvent(cell, EventTriggerType.PointerClick, delegate { OnClick(cell); });
            AddEvent(cell, EventTriggerType.PointerEnter, delegate { OnEnter(cell); });
            AddEvent(cell, EventTriggerType.PointerExit, delegate { OnExit(cell); });
            AddEvent(cell, EventTriggerType.BeginDrag, delegate { OnDragStart(cell); });
            AddEvent(cell, EventTriggerType.EndDrag, delegate { OnDragEnd(cell); });
            AddEvent(cell, EventTriggerType.Drag, delegate { OnDrag(cell); });
            return cell;
        }

        private void UpdateInventory()
        {
            // if (inventoryData.HasNothing())
            // {
            //     return;
            // }
            
            foreach (var item in inventoryData.Items)
            {
                int index = inventoryData.Items.IndexOf(item);
                var slot = inventorySlotsDisplayed[index];
                slot.ItemInSlot = inventoryData.Items[index];
                slot.RenderSlot();
            }

            Debug.Log("UPDATE INVENTORY");
        }

        private void RenderInventory()
        {
            for (int i = 0; i < inventoryData.Items.Capacity; i++)
            {
                var newSlot = CreateNewSlot(playerInventoryPanel);
                inventorySlotsDisplayed.Add(newSlot);
            }
            
        }

        private void RenderContainer()
        {
            for (int i = inventoryData.ContainerItems.Capacity; i >= 0; i--)
            {
                var newSlot = CreateNewSlot(containerPanel);
                containerSlotsDisplayed.Add(newSlot);
            }
        }

        #region EventTriggerMethods

        private void AddEvent(InventorySlot slot, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = slot.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnEnter(InventorySlot slot)
        {
            if (_mouseContainer.hoverSlot && slot.ItemInSlot != null)
            {
                if (_mouseContainer.hoverSlot.ItemInSlot == slot.ItemInSlot)
                {
                    slot.SetSelectionColor(Color.red);
                }
            }

            slot.EnableSelection(true);
            _mouseContainer.SetSlot(slot);
        }

        public void OnExit(InventorySlot slot)
        {
            slot.SetSelectionColor(Color.white);
            slot.EnableSelection(false);
            _mouseContainer.SetSlot(null);
        }

        public void OnDragStart(InventorySlot slot)
        {
            if (slot.ItemInSlot == null) return;
            Debug.Log("ONDRAG START");
            var mouseSlot = Instantiate(slot, inventoryWindow.transform); //может заменить родителя
            mouseSlot.ItemInSlot = slot.ItemInSlot;
            mouseSlot.RenderSlot();
            mouseSlot.SetOnDragState();
            _mouseContainer.SetHoverSlot(mouseSlot);
            slot.ItemInSlot = null;
            slot.RenderSlot();
        }

        public void OnDrag(InventorySlot slot)
        {
            if (_mouseContainer.hoverSlot == null) return;
            _mouseContainer.hoverSlot.transform.position = Input.mousePosition;
            Debug.Log("ONDRAG");
            // Debug.Log(_mouseContainer.hoverSlot);
            Debug.Log(_mouseContainer.hoverItem);
        }

        public void OnDragEnd(InventorySlot slot)
        {
            if (_mouseContainer.hoverSlot == null) return;
            if (_mouseContainer.pointSlot == null) // нет слота при отпускании
            {
                slot.ItemInSlot = _mouseContainer.hoverItem;
                slot.RenderSlot();
                Destroy(_mouseContainer.hoverSlot.gameObject);
            }
            else
            {
                // слот пустой 
                if (_mouseContainer.pointSlot.ItemInSlot == null)
                {
                    _mouseContainer.pointSlot.ItemInSlot = _mouseContainer.hoverItem;
                    _mouseContainer.pointSlot.RenderSlot();
                    
                }
                else // слот не пустой - СВАП предметов
                {
                    var cachedItem = _mouseContainer.pointItem;
                    _mouseContainer.pointSlot.ItemInSlot = _mouseContainer.hoverItem;
                    slot.ItemInSlot = cachedItem;
                    slot.RenderSlot();
                    _mouseContainer.pointSlot.RenderSlot();
                }
                
                Destroy(_mouseContainer.hoverSlot.gameObject);
                _mouseContainer.hoverSlot = null;
                _mouseContainer.hoverItem = null;
            }

            Debug.Log("ONDRAG END");
        }

        private void OnClick(InventorySlot slot)
        {
            if (slot.ItemInSlot!=null)
            {
                var item = slot.ItemInSlot;
                item.Use(inventoryData);
                // inventoryData.RemoveItem(item);
            }
        }

        #endregion
    }
}