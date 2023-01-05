using LiftGame.FPSController.CameraController;
using LiftGame.GameCore.Input.Data;
using LiftGame.Inventory;
using LiftGame.Inventory.Container;
using LiftGame.PlayerCore;
using UnityEngine;
using Zenject;

namespace LiftGame
{
    public class InventoryWindowController : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryWindow;
        [SerializeField] private ContainerPanelController containerController;
        [SerializeField] private GameObject containerPanel;
        private CameraController _cameraController;
        private IPlayerInventoryService _playerInventoryService;
        private IPlayerData _playerData;
        private bool _isContainerPanelActive = false;
        private bool _isWindowActive;

        //MonoBehaviour injection
        [Inject]
        private void Construct(PlayerServiceProvider playerServiceProvider,
            IPlayerInventoryService playerInventoryService)
        {
            _cameraController = playerServiceProvider.CameraController;
            _playerInventoryService = playerInventoryService;
        }

        void Start()
        {
            inventoryWindow.SetActive(false);
            _playerInventoryService.InventoryData.OnWorldContainerOpen += OpenContainerPanel;
            UIInputData.OnInventoryClicked += SwitchWindowState;
            NonGameplayInputData.OnPauseMenuClicked += CloseInventory;
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
            containerPanel.SetActive(_isContainerPanelActive);
        }
        
        private void OpenContainerPanel()
        {
            SwitchWindowState();
            _isContainerPanelActive = true;
            containerPanel.SetActive(_isContainerPanelActive);
        }
    }
}