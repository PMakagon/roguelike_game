using UnityEngine;

namespace LiftStateMachine.states
{
    public class MovingState : ILiftState
    {
        public void EnterState()
        {
            Debug.Log("Enter MovingState");
        }

        public void ExitState()
        {
            Debug.Log("Exit MovingState");
        }

        public void UpdateState()
        {
            Debug.Log("Update MovingState");
        }
    }
}