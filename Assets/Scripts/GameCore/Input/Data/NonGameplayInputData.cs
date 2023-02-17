using System;
using UnityEngine;

namespace LiftGame.GameCore.Input.Data
{
    [CreateAssetMenu(fileName = "NonGameplayInputData", menuName = "PlayerInputData/NonGameplayInputData")]
    public class NonGameplayInputData : ScriptableObject
    {
        private bool _pauseMenuClicked;
        private bool _pauseMenuReleased;
        private bool _tildeClicked;
        private bool _tildeReleased;
        public static event Action OnPauseMenuClicked = delegate { };
        public static event Action OnTildeClicked = delegate { };

        public void UpdateInputEvents()
        {
            if (_pauseMenuClicked) OnPauseMenuClicked?.Invoke();
            if (_tildeClicked) OnTildeClicked?.Invoke();
        }
        
        public bool TildeClicked
        {
            get => _tildeClicked;
            set => _tildeClicked = value;
        }

        public bool TildeReleased
        {
            get => _tildeReleased;
            set => _tildeReleased = value;
        }


        public bool PauseMenuClicked
        {
            get => _pauseMenuClicked;
            set => _pauseMenuClicked = value;
        }

        public bool PauseMenuReleased
        {
            get => _pauseMenuReleased;
            set => _pauseMenuReleased = value;
        }
        
        public void ResetInput()
        {
            _pauseMenuClicked = false;
            _pauseMenuReleased = false;
        }
    }
}