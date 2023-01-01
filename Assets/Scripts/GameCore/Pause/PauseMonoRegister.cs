using LiftGame.GameCore.Input;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.GameCore.Pause
{
    // удалить когда получится отвязаться от юнити евентов
    public class PauseMonoRegister : MonoBehaviour
    {
        private IPauseHandler _pauseHandler;
        private PlayerServiceProvider _playerServiceProvider;
        private IPlayerHealthService _playerHealthService;
        private IPlayerMentalService _playerMentalService;
        private IPlayerInputService _playerInputService;

        // MonoBehaviour injection
        [Inject]
        private void Construct(IPauseHandler pauseHandler, PlayerServiceProvider playerServiceProvider,
            IPlayerHealthService playerHealthService, IPlayerMentalService playerMentalService , IPlayerInputService inputService)
        {
            _playerHealthService = playerHealthService;
            _playerServiceProvider = playerServiceProvider;
            _pauseHandler = pauseHandler;
            _playerInputService = inputService;
            _playerMentalService = playerMentalService;
        }

        private void Start()
        {
            RegisterServices();
        }

        private void OnDestroy()
        {
            UnregisterServices();
        }

        private void RegisterServices()
        {
            _pauseHandler.Register(_playerServiceProvider.FPSController);
            _pauseHandler.Register(_playerInputService);
            _pauseHandler.Register(_playerServiceProvider.CameraController);
            _pauseHandler.Register(_playerHealthService);
            _pauseHandler.Register(_playerMentalService);
        }

        private void UnregisterServices()
        {
            _pauseHandler.UnRegister(_playerServiceProvider.FPSController);
            _pauseHandler.UnRegister(_playerInputService);
            _pauseHandler.UnRegister(_playerServiceProvider.CameraController);
            _pauseHandler.UnRegister(_playerHealthService);
            _pauseHandler.UnRegister(_playerMentalService);
        }
    }
}