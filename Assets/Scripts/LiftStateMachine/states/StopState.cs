using System;
using UnityEngine;

namespace LiftGame.LiftStateMachine.states
{
    public class StopState : ILiftState
    
    {
        public void EnterState()
        {
            Debug.Log("Enter StopState");
        }

        public void EnterState(Action enterAction)
        {
            Debug.Log("Enter StopState");
        }

        public void ExitState()
        {
            Debug.Log("Exit StopState");
        }

        public void ExitState(Action exitAction)
        {
            Debug.Log("Exit StopState");
        }

        public void SwitchState(ILiftState newState)
        {
            Debug.Log("Switched StopState to " + newState.GetType());
        }
    }
}