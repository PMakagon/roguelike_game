using System;
using System.Collections;
using LiftStateMachine.states;
using UnityEngine;

namespace LiftStateMachine
{
    public class InnerDoors : MonoBehaviour
    {
        [SerializeField] private LiftControllerData liftControllerData;
        private Animator _animator;
        private bool _isOpen;

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
            liftControllerData.IsDoorsOpen = true; 
            Debug.Log("Doors Open");
        }

        private void CloseDoors()
        {
            _animator.SetBool("IsOpened",liftControllerData.IsDoorsOpen);
            liftControllerData.IsDoorsOpen = false;
            Debug.Log("Doors Closed");
        }

        // public void OpenDoors()
        // {
        //     _isOpen = true;
        //     _animator.SetBool("IsOpened", _isOpen);
        //     liftControllerData.IsDoorsOpen = true; 
        //     Debug.Log("Doors Open");
        // }
        //
        // public void CloseDoors()
        // {
        //     _isOpen = false;
        //     _animator.SetBool("IsOpened", _isOpen);
        //     liftControllerData.IsDoorsOpen = false;
        //     Debug.Log("Doors Closed");
        // }
        
    }
}