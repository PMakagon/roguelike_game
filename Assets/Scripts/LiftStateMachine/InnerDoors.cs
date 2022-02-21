
using System;
using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class InnerDoors : MonoBehaviour
    {
        [SerializeField] private GameObject _door1;
        [SerializeField] private GameObject _door2;
        [SerializeField] private LiftControllerData liftControllerData;
        private Animator _animator;
        private bool _isOpen;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private bool AnimatorIsPlaying(){
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
        }
        
        public void ActivateDoors()
        {
            if (liftControllerData.CurrentState.GetType() == typeof(MovingState))
            {
                CloseDoors();
            }
            if (liftControllerData.CurrentState.GetType() == typeof(IdleState))
            {
                OpenDoors();
            }
        }

        public void OpenDoors()
        {
            _isOpen = true;
            _animator.SetBool("IsOpened",_isOpen);
            liftControllerData.IsDoorsOpen = true;
            Debug.Log("Doors Open");
            if (AnimatorIsPlaying())
            {
            }
        }

        public void CloseDoors()
        {
            _isOpen = false;
            _animator.SetBool("IsOpened",_isOpen);
            liftControllerData.IsDoorsOpen = false;
            Debug.Log("Doors Closed");
            if (AnimatorIsPlaying())
            {
            }
        }
    }
}