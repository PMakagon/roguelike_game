using System;
using LiftGame.GameCore.Input;
using LiftGame.PlayerCore;
using LiftGame.PlayerCore.HealthSystem;
using UnityEngine;
using Zenject;

namespace LiftGame.GameCore.Pause
{
    public class PauseMonoRegister : MonoBehaviour
    {
        private IPauseHandler _pauseHandler;
        private PlayerServiceProvider _playerServiceProvider;
        private IPlayerHealthService _playerHealthService;
        private IPlayerInputService _playerInputService;

        [Inject]
        private void Construct(IPauseHandler pauseHandler, PlayerServiceProvider playerServiceProvider,
            IPlayerHealthService playerHealthService, IPlayerInputService inputService)
        {
            _playerHealthService = playerHealthService;
            _playerServiceProvider = playerServiceProvider;
            _pauseHandler = pauseHandler;
            _playerInputService = inputService;
        }

        private void Start()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            _pauseHandler.Register(_playerServiceProvider.FPSController);
            _pauseHandler.Register(_playerInputService);
            _pauseHandler.Register(_playerServiceProvider.CameraController);
            _pauseHandler.Register(_playerHealthService);
        }

        private void UnregisterServices()
        {
            _pauseHandler.UnRegister(_playerServiceProvider.FPSController);
            _pauseHandler.UnRegister(_playerInputService);
            _pauseHandler.UnRegister(_playerServiceProvider.CameraController);
            _pauseHandler.UnRegister(_playerHealthService);
        }
    }
}