using System;
using LiftGame.Inventory.Core;
using LiftGame.Inventory.Items;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LiftGame.Ui
{
    public class InventoryContextMenu : MonoBehaviour
    {
        [SerializeField] private Button useBtn;
        [SerializeField] private Button equipBtn;
        [SerializeField] private Button unequipBtn;
        [SerializeField] private Button dropBtn;

        private ItemDefinition _item;
        private InventoryController _cachedController;
        private InventoryItemInteractor _interactor;

        [Inject] 
        public void Construct(InventoryItemInteractor interactor)
        {
            _interactor = interactor;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            useBtn.onClick.AddListener(UseItem);
            useBtn.onClick.AddListener(InventoryContextMenuController.Hide);
            equipBtn.onClick.AddListener(InventoryContextMenuController.Hide);
            dropBtn.onClick.AddListener(DropItem);
            dropBtn.onClick.AddListener(InventoryContextMenuController.Hide);
        }
        
        public void SetContextMenu(IInventoryItem item,InventoryController controller)
        {
            if(item==null) return;
            _item = item as ItemDefinition;
            _cachedController = controller;
            switch (_item.ItemType)
            {
                case ItemType.Consumable:
                    useBtn.gameObject.SetActive(true);
                    break;
                case ItemType.Equipment:
                    equipBtn.gameObject.SetActive(true);
                    break;
                case ItemType.Default:
                    break;
                case ItemType.PowerCell:
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

            if (_item.canDrop) dropBtn.interactable = true;
        }

        private void UseItem()
        {
            var consume = _item as ConsumableItem;
            if (consume == null) return;
            _interactor.UseItem(consume);
            if (consume.DestroyOnUse)
            {
                _cachedController.RepositoryManager.TryRemove(_item);
            }
        }

        private void Equip()
        {
            
        }

        private void DropItem()
        {
            _interactor.DropItem(_item);
            _cachedController.RepositoryManager.TryRemove(_item);
            _cachedController.RepositoryManager.TryForceDrop(_item);
            _cachedController.onItemDropped?.Invoke(_item);//??
        }

        public void ResetContextMenu()
        {
            _item = null;
            useBtn.gameObject.SetActive(false);
            // useBtn.onClick.RemoveAllListeners();
            equipBtn.gameObject.SetActive(false);
            // equipBtn.onClick.RemoveAllListeners();
            dropBtn.interactable = false;
            // dropBtn.onClick.RemoveAllListeners();
        }
        
    }
}