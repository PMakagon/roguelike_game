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

        [Inject]
        private void Construct(IPlayerCostumeService playerCostumeService,PlayerServiceProvider playerServiceProvider,IPlayerPowerService playerPowerService)
        {
            _playerCostumeService = playerCostumeService;
            _playerLitStateProvider = playerServiceProvider.PlayerLitStateProvider;
            _playerServiceProvider = playerServiceProvider;
            _playerPowerService = playerPowerService;
        }

        private void Start()
        {
            _playerServiceProvider.InputHandler.SetInputActive(true);
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