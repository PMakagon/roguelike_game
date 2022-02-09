using UnityEngine;

namespace LiftStateMachine.states
{
    public class IdleState : ILiftState
    {

        public void EnterState()
        {
            Debug.Log("Enter IdleState");
        }

        public void ExitState()
        {
            Debug.Log("Exit IdleState");
        }

        public void UpdateState()
        {
            Debug.Log("Update IdleState");
        }
    }
}