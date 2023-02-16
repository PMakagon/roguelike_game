using LiftGame.GameCore.GameFlow.App;
using UnityEngine;

namespace LiftGame.GameCore.GameFlow
{
    public class AppStateMachine : IStateMachine
    {
        private State _appState;

        public void Enter()
        {
            IsActive = true;
            SetState(new BootState(this));
        }

        public void SetState(State state)
        {
            if (!IsActive)
            {
                Debug.LogError("<color=red> STATE MACHINE: " + GetType().Name + " is not active  </color> ");
                return;
            }
            if (state == null) return;
            if (_appState != null)
            {
                Debug.Log("<color=red> [STATE] </color> EXIT " + _appState.GetType().Name);
                _appState?.OnExit();
            }
            _appState = state;
            Debug.Log("<color=red> [STATE] </color> ENTER " + _appState.GetType().Name);
            _appState?.OnEnter();
            if (_appState?.GetType() != typeof(ExitAppState)) return;
            IsFinal = true;
            Exit();
        }

        public void Exit()
        {
            IsActive = false;
        }

        public State GetState()
        {
            return _appState;
        }

        public bool IsActive { get;  set; }
        public bool IsFinal { get; private set; }
    }
}