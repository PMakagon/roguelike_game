using UnityEngine;

namespace LiftGame.Animations.Doors.StateBehaviours
{
    public class ForceOpenState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("ForceOpen",false);
            animator.SetBool("Open", true);
        }
    }
}