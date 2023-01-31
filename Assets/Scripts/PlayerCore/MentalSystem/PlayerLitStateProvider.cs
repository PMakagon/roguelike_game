using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LiftGame.GameCore.Pause;
using UnityEngine;
using Zenject;

namespace LiftGame.PlayerCore.MentalSystem
{
    public enum PlayerLitState
    {
        InTotalDark,
        DarkAhead,
        LightAhead,
        InShadow,
        Lit
    }

    public class PlayerLitStateProvider : MonoBehaviour,IPauseable
    {
        [SerializeField] private PlayerLightSensor playerSensor;
        [SerializeField] private PlayerLightSensor viewSensor;
        [SerializeField] private int updateRate;
        private PlayerMentalData _playerMentalData;
        private PlayerLitState _playerLitState;
        private IPauseHandler _pauseHandler;
        private CancellationTokenSource _cancellationToken = new();
        private bool _isActive;
        private bool _isPaused;


        //MonoBehaviour injection
        [Inject]
        private void Construct(IPlayerData playerData,IPauseHandler pauseHandler)
        {
            _playerMentalData = playerData.GetMentalData();
            _pauseHandler = pauseHandler;
        }

        private void Start()
        {
            _pauseHandler.Register(this);
        }

        private void OnDestroy()
        {
            _pauseHandler.UnRegister(this);
        }

        public void SetSensorsActive(bool state)
        {
            _isActive = state;
            playerSensor.gameObject.SetActive(state);
            viewSensor.gameObject.SetActive(state);
            if (_isActive)
            {
                EnableUpdate();
            }
            else
            {
                _cancellationToken.Cancel();
            }
        }

        private async void EnableUpdate()
        {
           await UpdateLitStatus(_cancellationToken.Token);
        }
        
        private async UniTask UpdateLitStatus(CancellationToken cancelToken)
        {
            while (_isActive)
            {
                if (_isPaused)
                {
                    await UniTask.WaitUntil(() => _isPaused == false, cancellationToken: cancelToken);
                }

                await UniTask.Delay(TimeSpan.FromMilliseconds(updateRate), ignoreTimeScale: false,
                    cancellationToken: cancelToken);
                _playerMentalData.PlayerLitState = GetPlayerLitState();
            }
        }
        

        private PlayerLitState GetPlayerLitState()
        {
            int player = playerSensor.Light;
            int view = viewSensor.Light;
            if (player + view <= 2)
            {
                _playerLitState = PlayerLitState.InTotalDark;
            }

            if (player + view <= 10 && player + view >= 5)
            {
                _playerLitState = PlayerLitState.InShadow;
            }

            if (player >= 10 && view <= 5)
            {
                _playerLitState = PlayerLitState.DarkAhead;
            }

            if (player <= 3 && view >= 4)
            {
                _playerLitState = PlayerLitState.LightAhead;
            }

            if (player >= 30 && view >= 10)
            {
                _playerLitState = PlayerLitState.Lit;
            }

            // Debug.Log(_playerLitState);
            return _playerLitState;
        }

        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}