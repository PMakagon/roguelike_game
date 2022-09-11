using UnityEngine;

namespace LiftGame.Animations.FirstPerson
{
    public class TurnOffStateBehaviour : StateMachineBehaviour
    {

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("TurnOff",false);
        }
    }
}
