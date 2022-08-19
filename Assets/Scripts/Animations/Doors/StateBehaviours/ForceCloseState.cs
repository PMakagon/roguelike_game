using UnityEngine;

namespace LiftGame.Animations.Doors.StateBehaviours
{
    public class ForceCloseState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("ForceClose",false);
            animator.SetBool("Open", false);
        }
    }
}