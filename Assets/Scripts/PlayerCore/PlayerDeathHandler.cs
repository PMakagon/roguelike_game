using LiftGame.FPSController.CameraController;
using LiftGame.FPSController.FirstPersonController;
using LiftGame.FX;
using LiftGame.GameCore.Input;
using LiftGame.GameCore.LevelGameLoop;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.ProxyEventHolders;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        private IPlayerMentalService _playerMentalService;
        private ILevelGameLoopEventHandler _gameLoopEventHandler;
        private IPlayerInputService _inputService;
        private FirstPersonController _fpsController;
        private CameraController _cameraController;
        
        // MonoBehaviour injection
        [Inject]
        private void Construct(ILevelGameLoopEventHandler gameLoopEventHandler,IPlayerInputService inputService)
        {
            _gameLoopEventHandler = gameLoopEventHandler;
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
            PlayerHealthEventHolder.OnPlayerDied += HandlePlayerDeath;
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