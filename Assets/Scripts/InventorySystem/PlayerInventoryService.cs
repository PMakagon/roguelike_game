using System.Collections.Generic;
using LiftGame.InventorySystem.Items;
using LiftGame.PlayerCore;
using UnityEngine;
using Zenject;

namespace LiftGame.InventorySystem
{
    public class PlayerInventoryService : IPlayerInventoryService
    {
        // private InventoryData _inventoryData;
        //
        // [Inject]
        // public PlayerInventoryService (IPlayerData playerData)
        // {
        //     _inventoryData = playerData.GetInventoryData();
        // }
        //
        // public int GetCapacity() => capacity;
        // public bool HasNothing() => _items.Count == 0;
        //
        // public bool IsFull() => _items.Count >= capacity;
        //
        // public void ClearContainer()
        // {
        //     _containerItems = null;
        //     _containerName = null;
        // }
        //
        // private void OnEnable()
        // {
        //     ResetData();
        // }
        //
        // public void ResetData()
        // {
        //     _items = new List<IItem>();
        //     _items.Capacity = capacity;
        // }
        //
        // public bool AddItem(IItem item, int amount)
        // {
        //     if (IsFull())
        //     {
        //         Debug.Log("inventory is full");
        //         return false;
        //     }
        //
        //     if (_items.Contains(item))
        //     {
        //         if (item.ItemType != ItemType.Consumable)
        //         {
        //             Debug.Log("Already Equipped " + item.Name);
        //             return false;
        //         }
        //     }
        //     _items.Add(item);
        //     onItemAdd?.Invoke();
        //     onInventoryChange?.Invoke();
        //     return true;
        // }
        //
        // public void RemoveItem(IItem item)
        // {
        //     _items.Remove(item);
        //     onInventoryChange?.Invoke();
        // }

    }
}