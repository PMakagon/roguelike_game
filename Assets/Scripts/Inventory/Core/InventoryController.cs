using System;
using LiftGame.Ui;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LiftGame.Inventory.Core
{
    /// <summary>
    /// Enables human interaction with an inventory renderer using Unity's event systems
    /// </summary>
    [RequireComponent(typeof(InventoryRenderer))]
    public class InventoryController : MonoBehaviour,
        IPointerDownHandler, IBeginDragHandler, IDragHandler,
        IEndDragHandler, IPointerExitHandler, IPointerEnterHandler,
        IInventoryController
    {
        // The dragged item is static and shared by all controllers
        // This way items can be moved between controllers easily
        private static InventoryDraggedItem _draggedItem;
        private Canvas _canvas;
        internal InventoryRenderer inventoryRenderer;
        private IInventoryItem _itemToDrag;
        private IInventoryItem _lastHoveredItem;
        private PointerEventData _currentEventData;
        internal IRepositoryManager RepositoryManager => inventoryRenderer.RepositoryManager;

        /// <inheritdoc />
        public Action<IInventoryItem> onItemHovered { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemPickedUp { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemAdded { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemSwapped { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemReturned { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemDropped { get; set; }

        void Awake()
        {
            inventoryRenderer = GetComponent<InventoryRenderer>();
            if (inventoryRenderer == null)
            {
                throw new NullReferenceException("Could not find a renderer. This is not allowed!");
            }

            // Find the canvas
            var canvases = GetComponentsInParent<Canvas>();
            if (canvases.Length == 0)
            {
                throw new NullReferenceException("Could not find a canvas.");
            }

            _canvas = canvases[canvases.Length - 1];
        }

        private IInventoryItem GetItemFromGrid()
        {
            var grid = ScreenToGrid(_currentEventData.position);
            var item = RepositoryManager.GetAtPoint(grid);
            return item;
        }

        private void Update()
        {
            if (_currentEventData == null) return; //not hovering over controller

            if (_draggedItem != null)
            {
                // Update position while dragging
                _draggedItem.position = _currentEventData.position;
                return;
            }
            // Detect hover
            var item = GetItemFromGrid();
            if (item == _lastHoveredItem) return;//also null & null
            if (item == null)
            {
                inventoryRenderer.ClearSelection();
                TooltipController.Hide();
            }
            else
            {
                TooltipController.Hide();
                TooltipController.Show(item);
                inventoryRenderer.HighlightItem(item);
                onItemHovered?.Invoke(item);
            }
            _lastHoveredItem = item;
            
        }

        /*
         * Grid was clicked (IPointerDownHandler)
         */

        public void OnPointerDown(PointerEventData eventData)
        {
            TooltipController.Hide();
            if (InventoryContextMenuController.IsOpen) InventoryContextMenuController.Hide();
            if (_draggedItem != null) return;
            // Get which item to drag (item will be null of none were found)
            var grid = ScreenToGrid(eventData.position);
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                InventoryContextMenuController.Show(RepositoryManager.GetAtPoint(grid),this);
                return;
            }

            _itemToDrag = RepositoryManager.GetAtPoint(grid);
        }

        /*
         * Dragging started (IBeginDragHandler)
         */

        public void OnBeginDrag(PointerEventData eventData)
        {
            inventoryRenderer.ClearSelection();

            if (_itemToDrag == null || _draggedItem != null) return;

            var localPosition = ScreenToLocalPositionInRenderer(eventData.position);
            var itemOffest = inventoryRenderer.GetItemOffset(_itemToDrag);
            var offset = itemOffest - localPosition;

            // Create a dragged item 
            _draggedItem = new InventoryDraggedItem(
                _canvas,
                this,
                _itemToDrag.position,
                _itemToDrag,
                offset
            );

            // Remove the item from inventory
            RepositoryManager.TryRemove(_itemToDrag);

            onItemPickedUp?.Invoke(_itemToDrag);
        }

        /*
         * Dragging is continuing (IDragHandler)
         */

        public void OnDrag(PointerEventData eventData)
        {
            _currentEventData = eventData;
            if (_draggedItem != null)
            {
                // Update the items position
                //_draggedItem.Position = eventData.position;
            }
        }

        /*
         * Dragging stopped (IEndDragHandler)
         */

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_draggedItem == null) return;

            var mode = _draggedItem.Drop(eventData.position);

            switch (mode)
            {
                case InventoryDraggedItem.DropMode.Added:
                    onItemAdded?.Invoke(_itemToDrag);
                    break;
                case InventoryDraggedItem.DropMode.Swapped:
                    onItemSwapped?.Invoke(_itemToDrag);
                    break;
                case InventoryDraggedItem.DropMode.Returned:
                    onItemReturned?.Invoke(_itemToDrag);
                    break;
                case InventoryDraggedItem.DropMode.Dropped:
                    onItemDropped?.Invoke(_itemToDrag);
                    ClearHoveredItem();
                    break;
            }

            _draggedItem = null;
        }

        /*
         * Pointer left the inventory (IPointerExitHandler)
         */

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_draggedItem != null)
            {
                // Clear the item as it leaves its current controller
                _draggedItem.currentController = null;
            }
            else
            {
                ClearHoveredItem();
            }

            inventoryRenderer.ClearSelection();
            TooltipController.Hide();
            _currentEventData = null;
        }

        /*
         * Pointer entered the inventory (IPointerEnterHandler)
         */

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_draggedItem != null)
            {
                // Change which controller is in control of the dragged item
                _draggedItem.currentController = this;
            }

            // TooltipController.Show(Repository.GetAtPoint(ScreenToGrid(eventData.position)));
            _currentEventData = eventData;
        }

        /* 
         * 
         */
        private void ClearHoveredItem()
        {
            if (_lastHoveredItem != null)
            {
                onItemHovered?.Invoke(null);
            }

            _lastHoveredItem = null;
        }

        /*
         * Get a point on the grid from a given screen point
         */
        internal Vector2Int ScreenToGrid(Vector2 screenPoint)
        {
            var pos = ScreenToLocalPositionInRenderer(screenPoint);
            var sizeDelta = inventoryRenderer.rectTransform.sizeDelta;
            pos.x += sizeDelta.x / 2;
            pos.y += sizeDelta.y / 2;
            return new Vector2Int(Mathf.FloorToInt(pos.x / inventoryRenderer.cellSize.x),
                Mathf.FloorToInt(pos.y / inventoryRenderer.cellSize.y));
        }

        private Vector2 ScreenToLocalPositionInRenderer(Vector2 screenPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                inventoryRenderer.rectTransform,
                screenPosition,
                _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
                out var localPosition
            );
            return localPosition;
        }
    }
}