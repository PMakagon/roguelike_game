using UnityEngine;

namespace LiftGame.Animations.FirstPerson
{
    public class TurnOnStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("TurnOn",false);
        }
    }
}
