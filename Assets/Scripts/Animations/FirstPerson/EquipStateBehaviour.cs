using UnityEngine;

namespace LiftGame.Animations.FirstPerson
{
    public class EquipStateBehaviour : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Equip",false);
        }
    }
}
