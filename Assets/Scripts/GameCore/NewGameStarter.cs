using LiftGame.GameCore.Input;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.PlayerCostume;
using UnityEngine;
using Zenject;

namespace LiftGame.GameCore
{
    public class NewGameStarter : MonoBehaviour
    {
        private IPlayerCostumeService _playerCostumeService;
        private PlayerServiceProvider _playerServiceProvider;
        private IPlayerInputService _playerInputService;

        // MonoBehaviour injection
        [Inject]
        private void Construct(PlayerServiceProvider playerServiceProvider,
            IPlayerInputService inputService)
        {
            _playerServiceProvider = playerServiceProvider;
            _playerInputService = inputService;
            _playerCostumeService = playerServiceProvider.CostumeService;
        }

        private void Start()
        {
            _playerInputService.SetInputActive(true);
            _playerServiceProvider.InventoryService.InitializeInventory();
        }


        public void GetPlayerStarted()
        {
            _playerCostumeService.SetCostumeActive(true);
            // _playerLitStateProvider.SetSensorsActive(true);
            Destroy(gameObject);
        }
    }
}