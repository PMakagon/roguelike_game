using UnityEngine;

namespace LiftGame.GameCore.GameFlow.Game
{
    public class GameStateMachine : IStateMachine
    {
        private State _gameState;

        public void Enter()
        {
            IsActive = true;
            SetState(new StartGameState(this));
        }

        public void SetState(State state)
        {
            if (state == null) return;
            if (_gameState != null)
            {
                Debug.Log("<color=red> [STATE] </color> EXIT " + _gameState.GetType().Name);
                _gameState?.OnExit();
            }
            _gameState = state;
            Debug.Log("<color=red> [STATE] </color> ENTER " + _gameState.GetType().Name);
            _gameState?.OnEnter();
            if (_gameState?.GetType() != typeof(GameExitState)) return;
            IsFinal = true;
            Exit();
        }

        public void Exit()
        {
            IsActive = false;
        }

        public State GetState()
        {
            return _gameState;
        }

        public bool IsActive { get; private set; }
        public bool IsFinal { get; private set; }
    }
}