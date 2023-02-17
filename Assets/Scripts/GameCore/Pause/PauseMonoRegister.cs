using LiftGame.GameCore.Input;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.HealthSystem;
using LiftGame.PlayerCore.MentalSystem;
using LiftGame.PlayerCore.PlayerAirSystem;
using LiftGame.PlayerCore.PlayerPowerSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.GameCore.Pause
{
    // удалить когда получится отвязаться от юнити евентов
    public class PauseMonoRegister : MonoBehaviour
    {
        private IPauseHandler _pauseHandler;
        private PlayerServiceProvider _playerServiceProvider;
        private IPlayerHealthService _healthService;
        private IPlayerMentalService _mentalService;
        private IPlayerInputService _inputService;
        private IPlayerAirService _airService;
        private IPlayerPowerService _powerService;

        // MonoBehaviour injection
        [Inject]
        private void Construct(IPauseHandler pauseHandler, PlayerServiceProvider serviceProvider,
            IPlayerHealthService healthService, IPlayerMentalService mentalService, IPlayerInputService inputService,
            IPlayerAirService airService, IPlayerPowerService powerService)
        {
            _healthService = healthService;
            _playerServiceProvider = serviceProvider;
            _pauseHandler = pauseHandler;
            _inputService = inputService;
            _mentalService = mentalService;
            _airService = airService;
            _powerService = powerService;
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
            _pauseHandler.Register(_inputService);
            _pauseHandler.Register(_playerServiceProvider.CameraController);
            _pauseHandler.Register(_healthService);
            _pauseHandler.Register(_mentalService);
            _pauseHandler.Register(_airService);
            _pauseHandler.Register(_powerService);
        }

        private void UnregisterServices()
        {
            _pauseHandler.UnRegister(_playerServiceProvider.FPSController);
            _pauseHandler.UnRegister(_inputService);
            _pauseHandler.UnRegister(_playerServiceProvider.CameraController);
            _pauseHandler.UnRegister(_healthService);
            _pauseHandler.UnRegister(_mentalService);
            _pauseHandler.UnRegister(_airService);
            _pauseHandler.UnRegister(_powerService);
        }
    }
}