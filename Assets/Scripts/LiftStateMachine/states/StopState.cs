using UnityEngine;

namespace LiftStateMachine.states
{
    public class StopState : ILiftState
    
    {
        public void EnterState()
        {
            Debug.Log("Enter StopState");
        }

        public void ExitState()
        {
            Debug.Log("Exit StopState");
        }

        public void UpdateState()
        {
            Debug.Log("Enter UpdateState");
        }
    }
}