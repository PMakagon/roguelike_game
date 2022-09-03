using LiftGame.FPSController.CameraController;
using LiftGame.FPSController.FirstPersonController;
using LiftGame.FPSController.InputHandler;
using LiftGame.GameCore.GameLoop;
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
        private FirstPersonController _fpsController;
        private InputHandler _inputHandler;
        private CameraController _cameraController;
        private ILevelGameLoopEventHandler _gameLoopEventHandler;

        [Inject]
        private void Construct(IPlayerHealthService playerHealthService,ILevelGameLoopEventHandler gameLoopEventHandler)
        {
            _gameLoopEventHandler = gameLoopEventHandler;
            _playerHealthService = playerHealthService;
        }

        private void Init()
        {
            var provider = GetComponent<PlayerServiceProvider>();
            _fpsController = provider.FPSController;
            _inputHandler = provider.InputHandler;
            _cameraController = provider.CameraController;
        }

        private void Start()
        {
            Init();
            _playerHealthService.OnPlayerDied += HandlePlayerDeath;
        }

        private void HandlePlayerDeath()
        {
            _inputHandler.SetInputActive(false);
            _cameraController.LockCursor(false);
            _fpsController.IsEnabled = false;
            _gameLoopEventHandler.GameOver();
        }
    }
}