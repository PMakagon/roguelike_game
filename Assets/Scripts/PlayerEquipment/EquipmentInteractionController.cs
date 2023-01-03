using LiftGame.GameCore.Input.Data;
using LiftGame.GameCore.Pause;
using LiftGame.Inventory;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerEquipment
{
    [RequireComponent(typeof(EquipmentSwitcher))]
    public class EquipmentInteractionController : MonoBehaviour,IPauseable
    {
        private InputDataProvider _inputDataProvider;
        private IPlayerInventoryService _inventoryService;
        private IPlayerPowerService _playerPowerService;
        private HeadFlashlight _headFlashlight;
        private EquipmentSwitcher _equipmentSwitcher;
        private IPauseHandler _pauseHandler;

        //MonoBehaviour injection
        [Inject]
        private void Construct(IPlayerInventoryService playerInventoryService, InputDataProvider inputDataProvider,
            IPlayerPowerService playerPowerService,IPauseHandler pauseHandler)
        {
            _inputDataProvider = inputDataProvider;
            _inventoryService = playerInventoryService;
            _playerPowerService = playerPowerService;
            _pauseHandler = pauseHandler;
        }

        private void Awake()
        {
            _headFlashlight = GetComponentInChildren<HeadFlashlight>();
            _equipmentSwitcher = GetComponent<EquipmentSwitcher>();
        }

        private void Start()
        {
            _headFlashlight.Initialize(_playerPowerService,_inputDataProvider);
            _equipmentSwitcher.Initialize(_inventoryService,_playerPowerService);
            _pauseHandler.Register(this);
            _inventoryService.OnInventoryLoad += GetStarted;
            _inventoryService.OnInventoryOpen += DisableInputListeners;
            _inventoryService.OnInventoryClose += EnableInputListeners;
            _inventoryService.InventoryData.OnWorldItemAddedToEquipmentSlot += GetStarted;
            EnableInputListeners();
        }
        
        private void OnDestroy()
        {
            _pauseHandler.UnRegister(this);
            _inventoryService.OnInventoryLoad -= GetStarted;
            _inventoryService.OnInventoryOpen -= DisableInputListeners;
            _inventoryService.OnInventoryClose -= EnableInputListeners;
            DisableInputListeners();
        }

        private void EnableInputListeners()
        {
            _inputDataProvider.EquipmentInputData.OnSwitchWeaponPressed += SwapEquipment;
            _inputDataProvider.EquipmentInputData.OnUsingClicked += UseEquipment;
            // _inputDataProvider.EquipmentInputData.OnTurnOnClicked += ;
            // _inputDataProvider.EquipmentInputData.OnTurnOnReleased += ;
           
        }

        private void DisableInputListeners()
        {
            _inputDataProvider.EquipmentInputData.OnSwitchWeaponPressed -= SwapEquipment;
            _inputDataProvider.EquipmentInputData.OnUsingClicked -= UseEquipment;
            // _inputDataProvider.EquipmentInputData.OnTurnOnClicked -= ;
            // _inputDataProvider.EquipmentInputData.OnTurnOnReleased -= ;
        }
        
        private void GetStarted()
        {
            _equipmentSwitcher.WithdrawEquipment();
            _inventoryService.InventoryData.OnWorldItemAddedToEquipmentSlot -= GetStarted;
        }

        private void SwapEquipment()
        {
            _equipmentSwitcher.SwapCurrentEquipment();
        }

        private void UseEquipment()
        {
           _inventoryService.GetCurrentEquipment()?.OnUse();
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused)
            {
                DisableInputListeners();
                return;
            }
            EnableInputListeners();
        }
    }
}