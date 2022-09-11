using LiftGame.FPSController.CameraController;
using LiftGame.FPSController.FirstPersonController;
using LiftGame.GameCore.GameLoop;
using LiftGame.GameCore.Input;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        private IPlayerHealthService _playerHealthService;
        private IPlayerMentalService _playerMentalService;
        private ILevelGameLoopEventHandler _gameLoopEventHandler;
        private IPlayerInputService _inputService;
        private FirstPersonController _fpsController;
        private CameraController _cameraController;

        [Inject]
        private void Construct(IPlayerHealthService playerHealthService,ILevelGameLoopEventHandler gameLoopEventHandler,IPlayerInputService inputService)
        {
            _gameLoopEventHandler = gameLoopEventHandler;
            _playerHealthService = playerHealthService;
            _inputService = inputService;
        }

        private void Init() // не круто
        {
            var provider = GetComponent<PlayerServiceProvider>();
            _fpsController = provider.FPSController;
            _cameraController = provider.CameraController;
        }

        private void Start()
        {
            Init();
            _playerHealthService.OnPlayerDied += HandlePlayerDeath;
        }

        private void HandlePlayerDeath()
        {
            _inputService.SetInputActive(false);
            _cameraController.LockCursor(false);
            _fpsController.IsEnabled = false;
            _gameLoopEventHandler.GameOver();
        }
    }
}