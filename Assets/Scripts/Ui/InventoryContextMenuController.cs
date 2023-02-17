using LiftGame.GameCore.Input.Data;
using LiftGame.Inventory;
using LiftGame.Inventory.Core;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace LiftGame.Ui
{
    public class InventoryContextMenuController : MonoBehaviour,
        IPointerDownHandler
    {
        [SerializeField] private InventoryContextMenu contextMenu;
        public static bool IsOpen;
        // private InventoryItemInteractor _interactor;
        //
        // [Inject]
        // public InventoryContextMenuController(InventoryItemInteractor interactor)
        // {
        //     _interactor = interactor;
        // }
        
        private static InventoryContextMenuController _currentContextMenuController;

        private void Awake()
        {
            if (!_currentContextMenuController)
            {
                _currentContextMenuController = this;
            }
            // _currentContextMenuController.contextMenu.Interactor = _interactor;
            UIInputData.OnInventoryClicked += Hide;
            NonGameplayInputData.OnPauseMenuClicked += Hide;
        }

        private void OnDestroy()
        {
            UIInputData.OnInventoryClicked -= Hide;
            NonGameplayInputData.OnPauseMenuClicked -= Hide;
        }

        public static void Show(IInventoryItem item,InventoryController controller)
        {
            if (item == null) return;
            IsOpen = true;
            _currentContextMenuController.contextMenu.gameObject.SetActive(true);
            _currentContextMenuController.contextMenu.SetContextMenu(item,controller);
            _currentContextMenuController.contextMenu.transform.position = Input.mousePosition;
            _currentContextMenuController.contextMenu.gameObject.SetActive(true);
        }

        public static void Hide()
        {
            IsOpen = false;
            _currentContextMenuController.contextMenu.ResetContextMenu();
            _currentContextMenuController.contextMenu.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsOpen) Hide();
        }
    }
}