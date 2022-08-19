using System;
using UnityEngine;

namespace LiftGame.LiftStateMachine.states
{
    public class IdleState : ILiftState
    {
        public void EnterState()
        {
            // Debug.Log("Enter IdleState");
        }

        public void EnterState(Action enterAction)
        {
            Debug.Log("Enter IdleState");
            enterAction.Invoke();
        }

        public void ExitState()
        {
            // Debug.Log("Exit IdleState");
        }

        public void ExitState(Action exitAction)
        {
            // Debug.Log("Exit IdleState");
        }

        public void SwitchState(ILiftState newState)
        {
            Debug.Log("Switched IdleState to " + newState.GetType());
        }
    }
}