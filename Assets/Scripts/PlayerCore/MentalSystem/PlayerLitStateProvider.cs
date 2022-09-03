using System;
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

    public class PlayerLitStateProvider : MonoBehaviour
    {
        [SerializeField] private PlayerLightSensor playerSensor;
        [SerializeField] private PlayerLightSensor viewSensor;
        private PlayerMentalData _playerMentalData;
        private PlayerLitState _playerLitState;
        private bool _isActive;


        [Inject]
        private void Construct(IPlayerData playerData)
        {
            _playerMentalData = playerData.GetMentalData();
        }

        public void SetSensorsActive(bool state)
        {
            _isActive = state;
            playerSensor.gameObject.SetActive(state);
            viewSensor.gameObject.SetActive(state);
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
               _playerLitState = GetPlayerLitState();
                _playerMentalData.PlayerLitState = _playerLitState;
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

            Debug.Log(_playerLitState);
            return _playerLitState;
            // _isTotalDark = player + view == 0;
            // _isDarkAhead = player >= 10 && view <= 5;
            // _isLightAhead = player == 0 && view >= 3;
            // _isInShadow = player <= 10 && view <= 10;
            // _isInLight = player >= 30 && view >= 10;
        }
    }
}