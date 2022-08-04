﻿using System;
using UnityEngine;

namespace InventorySystem.Items
{
    public enum ItemType
    {
        Default,
        Equipment,
        Consumable,
        Key,
        Quest
    }
    [Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
    public class Item : ScriptableObject, IItem
    { 
        [SerializeField] private string _name;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Sprite _uiicon;
        [SerializeField] private string _description;
        [SerializeField] private bool stackable;

        public bool Stackable
        {
            get => stackable;
            set => stackable = value;
        }
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public virtual ItemType ItemType
        {
            get => ItemType.Default;
            set => _itemType = value;
        }

        public Sprite UIIcon
        {
            get => _uiicon;
            set => _uiicon = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }
}