using UnityEngine;

namespace LiftGame.Animations.FirstPerson
{
    public class UnEquipStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("UnEquip",false);
        }
    }
}
