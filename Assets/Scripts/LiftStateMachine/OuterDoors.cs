using System;
using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class OuterDoors : MonoBehaviour
    {
        [SerializeField] private GameObject _door1;
        [SerializeField] private GameObject _door2;
        [SerializeField] private LiftControllerData liftControllerData;
        [SerializeField] private int ThisFloorNumber;
        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
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
            _animator.SetBool("IsOpened",liftControllerData.IsDoorsOpen);
        }

        public void CloseDoors()
        {
            _animator.SetBool("IsOpened",liftControllerData.IsDoorsOpen);
        }
    }
}