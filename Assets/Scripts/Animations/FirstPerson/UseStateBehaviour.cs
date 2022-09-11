using UnityEngine;

namespace LiftGame.Animations.FirstPerson
{
    public class UseStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Use",false);
        }
    
    }
}
