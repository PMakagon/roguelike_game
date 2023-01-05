using System;
using LiftGame.Inventory.Core;
using LiftGame.Inventory.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LiftGame.Ui
{
    public class InventoryContextMenu : MonoBehaviour
    {
        [SerializeField] private Button useBtn;
        [SerializeField] private Button equipBtn;
        [SerializeField] private Button unequipBtn;
        [SerializeField] private Button dropBtn;
        
        private ItemDefinition _item;

        public void SetContextMenu(IInventoryItem item)
        {
            if(item==null) return;
            _item = item as ItemDefinition;
            switch (_item.ItemType)
            {
                case ItemType.Consumable:
                    useBtn.gameObject.SetActive(true);
                    useBtn.onClick.AddListener((_item as ConsumableItem).Use);
                    useBtn.onClick.AddListener(InventoryContextMenuController.Hide);
                    break;
                case ItemType.Equipment:
                    equipBtn.gameObject.SetActive(true);
                    equipBtn.onClick.AddListener(InventoryContextMenuController.Hide);
                    break;
                case ItemType.Default:
                    break;
                case ItemType.Weapons:
                    break;
                case ItemType.Utility:
                    break;
                case ItemType.Key:
                    break;
                case ItemType.Quest:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_item.canDrop)
            {
                dropBtn.interactable = true;
                // dropBtn.onClick.AddListener( _item.SpawnWorldItem());
                dropBtn.onClick.AddListener(InventoryContextMenuController.Hide);
            }
        }

        public void ResetContextMenu()
        {
            useBtn.gameObject.SetActive(false);
            useBtn.onClick.RemoveAllListeners();
            equipBtn.gameObject.SetActive(false);
            equipBtn.onClick.RemoveAllListeners();
            dropBtn.interactable = false;
            dropBtn.onClick.RemoveAllListeners();
        }
        
    }
}