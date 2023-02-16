using LiftGame.FPSController.CameraController;
using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.LevelGameLoop;
using LiftGame.Inventory;
using LiftGame.Inventory.Container;
using LiftGame.PlayerCore;
using LiftGame.ProxyEventHolders.Player;
using UnityEngine;
using Zenject;

namespace LiftGame.Ui
{
    public class InventoryWindowController : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryWindow;
        [SerializeField] private ContainerPanelController containerController;
        private CameraController _cameraController;
        private IPlayerInventoryService _playerInventoryService;
        private IPlayerData _playerData;
        private bool _isContainerPanelActive = false;
        private bool _isWindowActive;

        //MonoBehaviour injection
        [Inject]
        private void Construct(PlayerServiceProvider playerServiceProvider)
        {
            _cameraController = playerServiceProvider.CameraController;
            _playerInventoryService = playerServiceProvider.InventoryService;
        }

        void Start()
        {
            inventoryWindow.SetActive(false);
            PlayerInventoryEventHolder.OnContainerOpen += OpenContainerPanel;
            UIInputData.OnInventoryClicked += SwitchWindowState;
            NonGameplayInputData.OnPauseMenuClicked += CloseInventory;
            LevelGameLoopEventHandler.OnGameOver += CloseInventory;
        }

        private void OnDestroy()
        {
            PlayerInventoryEventHolder.OnContainerOpen -= OpenContainerPanel;
            UIInputData.OnInventoryClicked -= SwitchWindowState;
            NonGameplayInputData.OnPauseMenuClicked -= CloseInventory;
            LevelGameLoopEventHandler.OnGameOver -= CloseInventory;
        }

        private void CloseInventory()
        {
            if (_isWindowActive) SwitchWindowState();
        }

        private void SwitchWindowState()
        {
            if (_isWindowActive) CloseContainerPanel();
            _isWindowActive = !_isWindowActive;
            inventoryWindow.SetActive(_isWindowActive);
            _cameraController.LockCursor(!_isWindowActive);
            _cameraController.LockCameraPosition(_isWindowActive);
            _playerInventoryService.SetInventoryOpen(_isWindowActive);
        }

        private void CloseContainerPanel()
        {
            _isContainerPanelActive = false;
            containerController.gameObject.SetActive(_isContainerPanelActive);
        }
        
        private void OpenContainerPanel()
        {
            SwitchWindowState();
            _isContainerPanelActive = true;
            containerController.gameObject.SetActive(_isContainerPanelActive);
        }
    }
}