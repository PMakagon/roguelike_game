using System;
using UnityEngine;

namespace LiftStateMachine.states
{
    public class MovingState : ILiftState
    {
        public void EnterState()
        {
            // Debug.Log("Enter MovingState");
        }

        public void EnterState(Action enterAction)
        {
            Debug.Log("Enter MovingState");
            enterAction.Invoke();
        }

        public void ExitState()
        {
            // Debug.Log("Exit MovingState");
        }

        public void ExitState(Action exitAction)
        {
            Debug.Log("Exit MovingState");
        }


        public void SwitchState(ILiftState newState)
        {
            Debug.Log("Switched MovingState to " + newState.GetType());
        }
    }
}