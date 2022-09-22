using LiftGame.FPSController.InteractionSystem;
using LiftGame.PlayerCore;
using UnityEngine;

namespace LiftGame.LiftStateMachine.Interactables
{
    public class FrontPanel : Interactable
    {
        [SerializeField] private Light buttonLight;
        [SerializeField] private LiftControllerData liftControllerData;
        
        private void Update()
        {
            buttonLight.enabled = liftControllerData.IsLiftCalled;
        }

        private void Awake()
        {
            buttonLight.enabled = false;
        }

        public override void OnInteract(IPlayerData playerData)
        {
            liftControllerData.IsLiftCalled = true;
            LiftControllerData.OnLiftCalled.Invoke();
        }
    }
}