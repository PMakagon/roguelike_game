using System;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using FPSController;


[CreateAssetMenu(fileName = "InventoryData", menuName = "InventorySystem/InventoryData")]
public class InventoryData : ScriptableObject
{
    [SerializeField] private int capacity = 3;
    private EquipableBase _equipable;
    [SerializeField]private List<Item> items;
    [SerializeField]private List<Key> keys;
    private bool isNeedUpdate = false;

    public void ResetData() => _equipable = null;
    public bool IsSameEquipable(EquipableBase _newEquipable) => _equipable == _newEquipable;
    
    public bool HasNothing() => items.Count == 0;
    public bool IsFull() => items.Count >= capacity;
    public bool IsEmpty() => _equipable == null;
    public void ClearInventory() => items.Clear();
    
    
    #region Methods
    
    private void Awake()
    {
        items = new List<Item>(capacity);
        keys = new List<Key>(10); ///определяется дизайном интерфейса
    }
    
    public void Equip()
    {
        _equipable.OnEquip(this);
        ResetData();
    }

    public bool AddItem(Item item)
    {
        if (IsFull())
        {
            Debug.Log("inventory is full");
            return false;
        }

        if (!items.Contains(item))
        {
            items.Add(item);
            isNeedUpdate = true;
            Debug.Log(" equipped: " + item.ToString());
            return true;
        }
        else
        {
            Debug.Log("Already Equipped " + item.Name);
            return false;
        }
    }
    
    // пройтись по двум массивам сразу
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        isNeedUpdate = true;
    }
    #endregion
    
    #region Properties
    public EquipableBase Equipable
    {
        get => _equipable;
        set => _equipable = value;
    }

    public bool IsNeedUpdate
    {
        get => isNeedUpdate;
        set => isNeedUpdate = value;
    }

    public List<Item> Items
    {
        get => items;
        set => items = value;
    }

    public List<Key> Keys
    {
        get => keys;
        set => keys = value;
    }
    #endregion
}