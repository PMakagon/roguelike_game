﻿using System;
using System.Linq;
using LiftGame.Inventory.Core;
using LiftGame.ProxyEventHolders.Player;
using UnityEngine;

namespace LiftGame.Inventory.Equipment
{
    public class EquipmentRepositoryManager : IRepositoryManager
    {
        private Vector2Int _size = new Vector2Int(2, 2);
        private IEquipmentRepository _repository;
        private Rect _fullRect;
        public event Action<int> OnItemRemovedFromSlot;

        public EquipmentRepositoryManager(IEquipmentRepository repository)
        {
            _repository = repository;
            Rebuild();
            Resize(width, height);
            PlayerInventoryEventHolder.OnItemAddedToEquipmentSlot += InvokeOnItemAdded;
        }

        private void InvokeOnItemAdded()
        {
            if (_repository.IsEmpty) return;
            var item = _repository.GetInventoryItem();
            ResizeSlotForItem(item);
            item.position = GetCenterPosition(item);
            Rebuild(false);
        }

        public int GetRepositorySlotId()
        {
            return _repository.SlotId;
        }

        private int _maxWigth = 3;

        private int _maxHeight = 3;

        /// <inheritdoc />
        public int width => _size.x;

        /// <inheritdoc />
        public int height => _size.y;

        /// <inheritdoc />
        public void Resize(int newWidth, int newHeight)
        {
            _size.x = newWidth;
            _size.y = newHeight;
            RebuildRect();
        }

        private void RebuildRect()
        {
            _fullRect = new Rect(0, 0, _size.x, _size.y);
            HandleSizeChanged();
            onResized?.Invoke();
        }

        private void HandleSizeChanged()
        {
            // // Drop all items that no longer fit the inventory
            // for (int i = 0; i < allItems.Length;)
            // {
            //     var item = allItems[i];
            //     var shouldBeDropped = false;
            //     var padding = Vector2.one * 0.01f;
            //
            //     if (!_fullRect.Contains(item.GetMinPoint() + padding) || !_fullRect.Contains(item.GetMaxPoint() - padding))
            //     {
            //         shouldBeDropped = true;
            //     }
            //
            //     if (shouldBeDropped)
            //     {
            //         TryDrop(item);
            //     }
            //     else
            //     {
            //         i++;
            //     }
            // }
        }

        /// <inheritdoc />
        public void Rebuild()
        {
            Rebuild(false);
        }


        private void Rebuild(bool silent)
        {
            allItems = new IInventoryItem[1] { _repository.GetInventoryItem() };
            if (!silent) onRebuilt?.Invoke();
        }

        public void Dispose()
        {
            _repository = null;
            allItems = null;
        }

        /// <inheritdoc />
        public bool isFull
        {
            get
            {
                if (_repository.IsEmpty) return false;

                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        if (GetAtPoint(new Vector2Int(x, y)) == null)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        /// <inheritdoc />
        public IInventoryItem[] allItems { get; private set; }

        /// <inheritdoc />
        public Action onRebuilt { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemDropped { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemDroppedFailed { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemAdded { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemAddedFailed { get; set; }

        /// <inheritdoc />
        public Action<IInventoryItem> onItemRemoved { get; set; }

        /// <inheritdoc />
        public Action onResized { get; set; }

        /// <inheritdoc />
        public IInventoryItem GetAtPoint(Vector2Int point)
        {
            // Single item override
            if (!_repository.IsEmpty)
            {
                return _repository.GetInventoryItem();
            }

            return null;
        }

        /// <inheritdoc />
        public IInventoryItem[] GetAtPoint(Vector2Int point, Vector2Int size)
        {
            var posibleItems = new IInventoryItem[size.x * size.y];
            var c = 0;
            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y < size.y; y++)
                {
                    posibleItems[c] = GetAtPoint(point + new Vector2Int(x, y));
                    c++;
                }
            }

            return posibleItems.Distinct().Where(x => x != null).ToArray();
        }

        /// <inheritdoc />
        public bool TryRemove(IInventoryItem item)
        {
            if (!CanRemove(item)) return false;
            if (!_repository.RemoveEquipmentItem()) return false;
            Resize(2, 2);
            Rebuild(true);
            onItemRemoved?.Invoke(item);
            OnItemRemovedFromSlot?.Invoke(_repository.SlotId);
            return true;
        }

        /// <inheritdoc />
        public bool TryDrop(IInventoryItem item)
        {
            if (!CanDrop(item) || !_repository.DropEquipmentItem())
            {
                onItemDroppedFailed?.Invoke(item);
                return false;
            }

            Rebuild(true);
            onItemDropped?.Invoke(item);
            return true;
        }

        public bool TryForceDrop(IInventoryItem item)
        {
            if (!item.canDrop)
            {
                onItemDroppedFailed?.Invoke(item);
                return false;
            }

            onItemDropped?.Invoke(item);
            return true;
        }

        /// <inheritdoc />
        public bool CanAddAt(IInventoryItem item, Vector2Int point)
        {
            return _repository.CanAddEquipmentItem(item) && _repository.IsEmpty;

            // var previousPoint = item.position;
            // item.position = point;
            // var padding = Vector2.one * 0.01f;
            //
            // // Check if item is outside of inventory
            // if (!_fullRect.Contains(item.GetMinPoint() + padding) || !_fullRect.Contains(item.GetMaxPoint() - padding))
            // {
            //     item.position = previousPoint;
            //     return false;
            // }
            //
            // // Check if item overlaps another item already in the inventory
            // if (!allItems.Any(otherItem => item.Overlaps(otherItem))) return true; // Item can be added
            // item.position = previousPoint;
            // return false;
        }

        private void ResizeSlotForItem(IInventoryItem item)
        {
            int newWidht = _size.x;
            int newHeight = _size.y;
            if (item.height > _size.y && item.height <= _maxHeight)
            {
                newHeight = item.height;
                if (item.width > _size.x && item.width <= _maxWigth)
                {
                    newWidht = item.width;
                }
            }

            Resize(newWidht, newHeight);
        }

        /// <inheritdoc />
        public bool TryAddAt(IInventoryItem item, Vector2Int point)
        {
            if (!CanAddAt(item, point) || !_repository.AddEquipmentItem(item))
            {
                onItemAddedFailed?.Invoke(item);
                return false;
            }

            ResizeSlotForItem(item);
            item.position = GetCenterPosition(item);
            Rebuild(true);
            onItemAdded?.Invoke(item);
            return true;
        }

        /// <inheritdoc />
        public bool CanAdd(IInventoryItem item)
        {
            Vector2Int point;
            if (!Contains(item) && GetFirstPointThatFitsItem(item, out point))
            {
                return CanAddAt(item, point);
            }

            return false;
        }

        /// <inheritdoc />
        public bool TryAdd(IInventoryItem item)
        {
            if (!CanAdd(item)) return false;
            Vector2Int point;
            return GetFirstPointThatFitsItem(item, out point) && TryAddAt(item, point);
        }

        /// <inheritdoc />
        public bool CanSwap(IInventoryItem item)
        {
            return DoesItemFit(item) && _repository.CanAddEquipmentItem(item);
        }

        /// <inheritdoc />
        public void DropAll()
        {
            var itemsToDrop = allItems.ToArray();
            foreach (var item in itemsToDrop)
            {
                TryDrop(item);
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            foreach (var item in allItems)
            {
                TryRemove(item);
            }
        }

        /// <inheritdoc />
        public bool Contains(IInventoryItem item) => allItems.Contains(item);


        /// <inheritdoc />
        public bool CanRemove(IInventoryItem item) => Contains(item) && _repository.CanRemoveEquipmentItem();

        /// <inheritdoc />
        public bool CanDrop(IInventoryItem item) =>
            Contains(item) && _repository.CanDropEquipmentItem() && item.canDrop;

        /*
         * Get first free point that will fit the given item
         */
        private bool GetFirstPointThatFitsItem(IInventoryItem item, out Vector2Int point)
        {
            if (DoesItemFit(item))
            {
                for (var x = 0; x < width - (item.width - 1); x++)
                {
                    for (var y = 0; y < height - (item.height - 1); y++)
                    {
                        point = new Vector2Int(x, y);
                        if (CanAddAt(item, point)) return true;
                    }
                }
            }

            point = Vector2Int.zero;
            return false;
        }

        /* 
         * Returns true if given items physically fits within this inventory
         */
        private bool DoesItemFit(IInventoryItem item) => item.width <= width && item.height <= height;

        /*
         * Returns the center post position for a given item within this inventory
         */
        private Vector2Int GetCenterPosition(IInventoryItem item)
        {
            return new Vector2Int(
                (_size.x - item.width) / 2,
                (_size.y - item.height) / 2
            );
        }
    }
}