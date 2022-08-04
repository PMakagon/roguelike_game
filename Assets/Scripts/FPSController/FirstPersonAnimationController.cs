using System;
using FPSController.Scriptable_Objects;
using UnityEngine;

namespace FPSController
{
    public class FirstPersonAnimationController : MonoBehaviour

    {
        [SerializeField] private CameraInputData cameraInputData;
        [SerializeField] private MovementInputData movementInputData ;
        [SerializeField] private InteractionInputData interactionInputData;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            // if (movementInputData.IsCrouching)
            // {
            //     _animator.SetBool("IsCrouching",movementInputData.IsCrouching);
            // }
            // else
            // {
            //     _animator.SetBool("IsCrouching",movementInputData.IsCrouching);
            // }
            
        }
    }
}