using LiftGame.GameCore.Input.Data;
using LiftGame.Inventory.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LiftGame.Ui
{
    public class InventoryContextMenuController : MonoBehaviour,
        IPointerDownHandler
    {
        [SerializeField] private InventoryContextMenu contextMenu;
        public static bool IsOpen;

        private static InventoryContextMenuController _currentContextMenuController;

        private void Awake()
        {
            if (!_currentContextMenuController)
            {
                _currentContextMenuController = this;
            }

            UIInputData.OnInventoryClicked += Hide;
            NonGameplayInputData.OnPauseMenuClicked += Hide;
        }

        private void OnDestroy()
        {
            UIInputData.OnInventoryClicked -= Hide;
            NonGameplayInputData.OnPauseMenuClicked -= Hide;
        }

        public static void Show(IInventoryItem item)
        {
            if (item == null) return;
            IsOpen = true;
            _currentContextMenuController.contextMenu.gameObject.SetActive(true);
            _currentContextMenuController.contextMenu.SetContextMenu(item);
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