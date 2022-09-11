using LiftGame.FPSController.ScriptableObjects;
using LiftGame.GameCore.Input.Data;
using UnityEngine;

namespace LiftGame.FPSController
{
    public class PlayerAnimationController : MonoBehaviour

    {
        [SerializeField] private CameraInputData cameraInputData;
        [SerializeField] private MovementInputData movementInputData ;
        [SerializeField] private InteractionInputData interactionInputData;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        
    }
}