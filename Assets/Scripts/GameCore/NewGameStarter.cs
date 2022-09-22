using LiftGame.GameCore.Input;
using LiftGame.NewInventory;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerCostume;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.GameCore
{
    public class NewGameStarter : MonoBehaviour
    {
        private IPlayerCostumeService _playerCostumeService;
        private PlayerLitStateProvider _playerLitStateProvider;
        private PlayerServiceProvider _playerServiceProvider;
        private IPlayerPowerService _playerPowerService;
        private IPlayerInputService _playerInputService;
        private IPlayerInventoryService _inventoryService;

        [Inject]
        private void Construct(IPlayerCostumeService costumeService, PlayerServiceProvider playerServiceProvider,
            IPlayerPowerService powerService, IPlayerInputService inputService,IPlayerInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _playerCostumeService = costumeService;
            _playerLitStateProvider = playerServiceProvider.PlayerLitStateProvider;
            _playerServiceProvider = playerServiceProvider;
            _playerPowerService = powerService;
            _playerInputService = inputService;
        }

        private void Start()
        {
            _playerInputService.SetInputActive(true);
            _inventoryService.InitializeInventory();
        }


        public void GetPlayerStarted()
        {
            _playerCostumeService.SetCostumeActive(true);
            _playerLitStateProvider.SetSensorsActive(true);
            _playerPowerService.SetActive(true);
            Destroy(gameObject);
        }
    }
}