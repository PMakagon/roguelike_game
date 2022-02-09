using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class LiftControllerData : ScriptableObject
    {
        private ILiftState _liftState;
        private int _waitingTime = 10;

        public int WaitingTime => _waitingTime;

        public ILiftState LiftState
        {
            get => _liftState;
            set => _liftState = value;
        }
    }
}