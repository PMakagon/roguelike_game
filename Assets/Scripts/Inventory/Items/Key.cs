﻿using UnityEngine;

namespace LiftGame.Inventory.Items
{
    [CreateAssetMenu(fileName = "Key", menuName = "Player/InventorySystem/Items/Key")]
    public class Key : ItemDefinition
    {
        [SerializeField] private string keyCode;

        public string KeyCode
        {
            get => keyCode;
            set => keyCode = value;
        }

        public override ItemType ItemType => ItemType.Key;

        private void Awake()
        {
            ItemType = ItemType.Key;
        }
    }
}