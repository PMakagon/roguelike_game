using System;
using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class OuterDoors : MonoBehaviour
    {
        [SerializeField] private LiftControllerData liftControllerData;
        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            liftControllerData.OnDoorsAction += ActivateDoors;
        }

        private void OnDisable()
        {
            liftControllerData.OnDoorsAction -= ActivateDoors;
        }


        public void ActivateDoors()
        {
            if (liftControllerData.IsDoorsOpen)
            {
                OpenDoors();
            }
            else
            {
                CloseDoors();
            }
        }

        private void OpenDoors()
        {
            _animator.SetBool("IsOpened",liftControllerData.IsDoorsOpen);
        }

        private void CloseDoors()
        {
            _animator.SetBool("IsOpened",liftControllerData.IsDoorsOpen);
        }
    }
}