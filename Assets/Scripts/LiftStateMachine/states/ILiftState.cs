using System;

namespace LiftStateMachine.states
{
    public interface ILiftState
    {
        void EnterState();
        void EnterState(Action enterAction);
        void ExitState();
        void ExitState(Action exitAction);
        void SwitchState(ILiftState newState);
    }
}